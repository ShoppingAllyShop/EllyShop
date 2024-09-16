using CategoryEntity = Comman.Domain.Models.Category;
using System.Collections;
using Category.Api.Models.Request;

namespace Category.Api.Interfaces
{
    public interface ICategory
    {
        Task<IEnumerable<CategoryEntity>> GetAll();
        Task<string> AddCategoryAsync (AddCategoryRequest request);
    }
}
