using CategoryEntity = Comman.Domain.Models.Category;
using System.Collections;

namespace Category.Api.Interfaces
{
    public interface ICategory
    {
       Task<IEnumerable<CategoryEntity>> GetAll();
    }
}
