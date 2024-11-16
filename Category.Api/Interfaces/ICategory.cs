using CategoryEntity = Comman.Domain.Elly_Catalog.Category;
using System.Collections;
using Catalog.Api.Models.CategoryModel.Request;


namespace Category.Api.Interfaces
{
    public interface ICategory
    {
        Task<IEnumerable<CategoryEntity>> GetAllAsync();
        Task<string> AddCategoryAsync (CategoryRequest request);
        Task<string> DeleteCategoryAsync(Guid id, string name);
        Task<CategoryEntity> EditCategoryAsync(CategoryRequest request);
    }
}
