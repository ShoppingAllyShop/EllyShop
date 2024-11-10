using Catalog.Api.Models.CatalogModel.Response;

namespace Catalog.Api.Interfaces
{
    public interface ICatalog
    {
        Task<CatalogDataResponse> GetMainPageContent();
    }
}
