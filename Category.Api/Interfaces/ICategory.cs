using CategoryEntity = Comman.Domain.Models.Category;
using System.Collections;
using Category.Api.Models.Request;
using Comman.Domain.Models;

namespace Category.Api.Interfaces
{
    public interface ICategory
    {
        Task<IEnumerable<CategoryEntity>> GetAll();
        Task<string> AddCategoryAsync (CategoryRequest request);
        Task<string> DeleteCategoryAsync(Guid id, string name);
        Task<CategoryEntity> EditCategoryAsync(CategoryRequest request);
        //Task<Product> GetProductAsync(string id);

    }
}
