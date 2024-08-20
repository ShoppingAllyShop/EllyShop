using Category.Api.Interfaces;
using Comman.Domain.Models;
using Common.Infrastructure;
using Common.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using CategoryEntity = Comman.Domain.Models.Category;
namespace Category.Api.Implements
{
    public class CategoryServices : ICategory
    {
        private readonly IUnitOfWork<EllyShopContext> _unitOfWork;
        public CategoryServices(IUnitOfWork<EllyShopContext> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<CategoryEntity>> GetAll()
        {
           var result = await _unitOfWork.Repository<CategoryEntity>().AsNoTracking().ToListAsync();
            return result;
        }
    }
}

