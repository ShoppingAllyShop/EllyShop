
using Comman.Domain.Elly_Catalog;

namespace Catalog.Api.Interfaces
{
    public interface ICollections
    {
        Task<IEnumerable<Collection>> GetCollectionAsync();
    }
}
