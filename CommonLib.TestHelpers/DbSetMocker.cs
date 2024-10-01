using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net.Sockets;

namespace CommonLib.TestHelpers
{
    public static class DbSetMocker
    {

        public static Mock<DbSet<T>> Create<T>(params T[] elements) where T : class
        {
            return new List<T>(elements).AsQueryable().MockDbSet();
        }

        public static Mock<DbSet<T>> MockDbSet<T>(this IQueryable<T> testData) where T : class
        {
            Mock<DbSet<T>> dbSetMock = new Mock<DbSet<T>>();
            dbSetMock.As<IQueryable<T>>().Setup(m => m.Provider).Returns(new TestAsyncQueryProvider<T>(testData.Provider));
            dbSetMock.As<IQueryable<T>>().Setup(m => m.Expression).Returns(testData.Expression);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(testData.ElementType);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(testData.GetEnumerator());
            dbSetMock.As<IAsyncEnumerable<T>>().Setup(m => m.GetAsyncEnumerator(default)).Returns(new TestAsyncEnumerator<T>(testData.GetEnumerator()));
            dbSetMock.Setup(d => d.RemoveRange(It.IsAny<IEnumerable<T>>())).Callback<IEnumerable<T>>((s) => {
                var lst = testData.ToList();
                foreach (var i in s)
                {
                    lst.Remove(i);
                }
                testData = lst.AsQueryable();
                dbSetMock.As<IQueryable<T>>().Setup(m => m.Provider).Returns(new TestAsyncQueryProvider<T>(testData.Provider));
                dbSetMock.As<IQueryable<T>>().Setup(m => m.Expression).Returns(testData.Expression);
                dbSetMock.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(testData.ElementType);
            });
            return dbSetMock;
        }

        public static Mock<DbSet<T>> MockDbSet<T>(this IEnumerable<T> elements) where T : class
        {
            var testData = elements.AsQueryable();
            return testData.MockDbSet();
        }

        public static Mock<DbSet<T>> MockDbSet<T>(this T[] elements) where T : class
        {
            var testData = elements.AsQueryable();
            return testData.MockDbSet();
        }

        public static Mock<DbSet<TQuery>> MockDbQuery<TQuery>(this IQueryable<TQuery> testData) where TQuery : class
        {
            var dbQueryMock = new Mock<DbSet<TQuery>>();

            dbQueryMock.As<IQueryable<TQuery>>().Setup(m => m.ElementType).Returns(testData.ElementType);
            dbQueryMock.As<IQueryable<TQuery>>().Setup(m => m.ElementType).Returns(testData.ElementType);
            dbQueryMock.As<IQueryable<TQuery>>().Setup(m => m.Expression).Returns(testData.Expression);
            dbQueryMock.As<IEnumerable>().Setup(m => m.GetEnumerator()).Returns(testData.GetEnumerator());
            dbQueryMock.As<IEnumerable<TQuery>>().Setup(m => m.GetEnumerator()).Returns(testData.GetEnumerator());
            dbQueryMock.As<IQueryable<TQuery>>().Setup(x => x.Provider).Returns(new TestAsyncQueryProvider<TQuery>(testData.Provider));
            dbQueryMock.As<IAsyncEnumerable<TQuery>>().Setup(m => m.GetAsyncEnumerator(default)).Returns(new TestAsyncEnumerator<TQuery>(testData.GetEnumerator()));

            return dbQueryMock;
        }

        public static DbSet<T> MockFromSql<T>(this SpAsyncEnumerableQueryable<T> spItems) where T : class
        {
            var queryProviderMock = new Mock<IQueryProvider>();
            queryProviderMock.Setup(p => p.CreateQuery<T>(It.IsAny<MethodCallExpression>()))
                .Returns<MethodCallExpression>(x =>
                {
                    return spItems;
                });

            var dbSetMock = new Mock<DbSet<T>>();
            dbSetMock.As<IQueryable<T>>()
                .SetupGet(q => q.Provider)
                .Returns(() =>
                {
                    return queryProviderMock.Object;
                });

            dbSetMock.As<IQueryable<T>>()
                .Setup(q => q.Expression)
                .Returns(Expression.Constant(dbSetMock.Object));
            return dbSetMock.Object;
        }

        public static Mock<DbSet<T>> MockDbSet<T>(this List<T> list) where T : class
        {
            IQueryable<T> testData = list.AsQueryable();
            return testData.MockDbSet();
        }
    }

    public class SpAsyncEnumerableQueryable<T> : IAsyncEnumerable<T>, IQueryable<T>
    {
        private IAsyncEnumerable<T> _spItemsAsync;
        private IEnumerable<T> _spItems;
        public Expression Expression => throw new NotImplementedException();
        public Type ElementType => throw new NotImplementedException();
        public IQueryProvider Provider => throw new NotImplementedException();

        public IAsyncEnumerator<T> GetEnumerator()
        {
            return _spItemsAsync.GetAsyncEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _spItems.GetEnumerator();
        }

        IAsyncEnumerator<T> IAsyncEnumerable<T>.GetAsyncEnumerator(CancellationToken cancellationToken) => _spItemsAsync.GetAsyncEnumerator(default);

        public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            return new TestAsyncEnumerator<T>(this.AsEnumerable().GetEnumerator());
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return _spItems.GetEnumerator();
        }
    }
}
