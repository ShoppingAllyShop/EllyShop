using Comman.Domain.Elly_Catalog;
using CommonLib.Models.Base;

namespace Catalog.Api.Models.CollectionModel.Response
{
    public class SearchCollectionResponse
    {
        public PagingResponseBase? Paging { get; set; }
        public IEnumerable<Collectioninfo>? CollectionList { get; set; }
    }
    public class Collectioninfo
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}
