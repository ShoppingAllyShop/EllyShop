using Category.Api.Interfaces;
using Category.Api.Models.Request;
using Comman.Domain.Models;
using Common.Infrastructure;
using Common.Infrastructure.Interfaces;
using CommonLib.Exceptions;
using Microsoft.EntityFrameworkCore;
using CategoryEntity = Comman.Domain.Models.Category;
namespace Category.Api.Implements
{
    public class CategoryServices : ICategory
    {
        private readonly IUnitOfWork<EllyShopContext> _unitOfWork;
        private readonly ILogger<CategoryServices> _logger;
        public CategoryServices(IUnitOfWork<EllyShopContext> unitOfWork, ILogger<CategoryServices> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<string> AddCategoryAsync(AddCategoryRequest request)
        {
            var isExistCate = await _unitOfWork.Repository<CategoryEntity>()
                .AsNoTracking()
                .AnyAsync(x => x.Name == request.Name && x.CategoryLevel == request.Level);
            if (isExistCate) throw new BusinessException("Danh mục đã tồn tại");

            var newCategory = new CategoryEntity()
            {
                Id = Guid.NewGuid(),
                CategoryLevel = request.CategoryLevel,
                Name = request.Name,
                Gender = request.Gender,
                ParentId = request.ParentId
            };
            await _unitOfWork.Repository<CategoryEntity>().AddAsync(newCategory);
            var saveResult = await _unitOfWork.SaveChangesAsync();

            if (saveResult > 0)
            {
                _logger.LogInformation($"Create new category successfully: {newCategory}");
                return newCategory.Name;
            }
            return string.Empty;
        }

        public async Task<IEnumerable<CategoryEntity>> GetAll()
        {
           var result = await _unitOfWork.Repository<CategoryEntity>().AsNoTracking().ToListAsync();
            return result;
        }
    }
}

