
using Catalog.Api.Models.CollectionModel.Request;
using Catalog.Api.Models.CollectionModel.Response;
using Comman.Domain.Elly_Catalog;

namespace Catalog.Api.Interfaces
{
    public interface ICollections
    {
        Task<SearchCollectionResponse> SearchCollectionAsync(int? pageNumber, int? pageSize, string? sortBy, string? sortOrder, string? searchInput); 
        Task<DeleteCollectionReponse> DeleteCollectionAsync(DeleteCollectionRequest model);
        Task<Collection> EditCollectionAsync(CollectionRequest request);
        Task<string> AddCollectionAsync(CollectionRequest request);
        Task<DataCollectionResponse> GetDataAdminCollectionPageAsync();
        Task<IEnumerable<Collection>> GetCollectionAsync();
    }
}
