using Catalog.Api.Models.ProductModel.Respone;
using Catalog.Api.Models.CategoryModel.Response;
using Catalog.Api.Models.CollectionModel.Response;
using Comman.Domain.Elly_Catalog;
using CategoryEntity = Comman.Domain.Elly_Catalog.Category;

namespace Catalog.Api.Models.CatalogModel.Response
{

    public class CatalogDataResponse
    {
        public MainPageProductResponse? ProductList { get; set; }
        public IEnumerable<Collection>? CollectionsList { get; set; }
        public IEnumerable<CategoryEntity>? CategoryList { get; set; }

    }
}
