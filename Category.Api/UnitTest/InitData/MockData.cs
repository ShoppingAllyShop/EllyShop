using Comman.Domain.Elly_Catalog;
using Guarantees = Comman.Domain.Elly_Catalog.Guarantee;

namespace Catalog.Api.UnitTest.InitData
{
    public static class MockData
    {
        public static List<Guide> CreateGuideDataData()
        {
            return new List<Guide>
            {
                new Guide
                {
                    Id = Guid.NewGuid(),
                    Title = "Title1",
                    GuideContent = "Cam kết sản phẩm đúng chất lượng miêu tả trên website."
                },
                new Guide
                {
                    Id = Guid.NewGuid(),
                    Title = "Title2",
                    GuideContent = "Content 2"
                },
            };
        }
        public static List<Guarantees> CreateGuaranteeData()
        {
            return new List<Guarantees>
            {
                new Guarantees
                {
                    Id = Guid.NewGuid(),
                    Contents = "Cam kết sản phẩm đúng chất lượng miêu tả trên website.",
                    Title = "Title1"
                },
                new Guarantees
                {
                    Id = Guid.NewGuid(),
                    Contents = "Content 2",
                    Title = "Title2"
                },
            };
        }
        public static List<Rating> CreateRatingData()
        {
            return new List<Rating>
            {

                new Rating
                {
                    Id = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    ProductId = "EC001",
                    Point = 1
                },
                new Rating
                {
                    Id = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    ProductId = "EC001",
                    Point = 1
                },
            };
        }
    }
}
