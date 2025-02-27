using AutoMapper;
using Catalog.Api.Models.CategoryModel.Request;
using Category.Api.Interfaces;
using Comman.Domain.Elly_Catalog;
using Comman.Domain.Models;
using Common.Infrastructure;
using Common.Infrastructure.Interfaces;
using CommonLib.Exceptions;
using Microsoft.EntityFrameworkCore;
using CategoryEntity = Comman.Domain.Elly_Catalog.Category;
namespace Category.Api.Implements
{
    public class CategoryServices : ICategory
    {
        private readonly IUnitOfWork<Elly_CatalogContext> _unitOfWork;
        private readonly ILogger<CategoryServices> _logger;
        private readonly IMapper _mapper;
        public CategoryServices(IUnitOfWork<Elly_CatalogContext> unitOfWork, ILogger<CategoryServices> logger,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryEntity>> GetAllAsync()
        {
            var result = await _unitOfWork.Repository<CategoryEntity>().AsNoTracking().ToListAsync();
            return result;
        }

        public async Task<string> AddCategoryAsync(CategoryRequest request)
        {
            var isExistCate = await _unitOfWork.Repository<CategoryEntity>()
                .AsNoTracking()
                .AnyAsync(x => x.Name == request.Name && x.CategoryLevel == request.CategoryLevel);
            if (isExistCate) throw new BusinessException("Danh mục đã tồn tại");

            var newCategory = new CategoryEntity()
            {
                Id = request.Id,
                CategoryLevel = request.CategoryLevel,
                Name = request.Name,
                Gender = request.Gender,
                ParentId = request.ParentId
            };
            await _unitOfWork.Repository<CategoryEntity>().AddAsync(newCategory);
            var saveResult = await _unitOfWork.SaveChangesAsync();
    
            return newCategory.Name;
        }

        public async Task<string> DeleteCategoryAsync(Guid id, string name)
        {
            var category = await _unitOfWork.Repository<CategoryEntity>()
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
            if (category == null) throw new BusinessException("Danh mục này không tồn tại");

            _unitOfWork.Repository<CategoryEntity>().Remove(category);
            await _unitOfWork.SaveChangesAsync();

            _logger.LogInformation($"Remove category successfully.Id: {id}, Name: {name}");
            return name;
        }

        public async Task<CategoryEntity> EditCategoryAsync(CategoryRequest request)
        {
            var category = await _unitOfWork.Repository<CategoryEntity>()
               .AsNoTracking()
               .FirstOrDefaultAsync(x => x.Id == request.Id);
            if (category == null) throw new BusinessException("Danh mục này không tồn tại");

            var updatedCategory = _mapper.Map<CategoryEntity>(request);
            _unitOfWork.UpdateEntity(updatedCategory);
            await _unitOfWork.SaveChangesAsync();
            return updatedCategory;
        }

    }
}

