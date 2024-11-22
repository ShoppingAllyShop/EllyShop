using Catalog.Api.Models.CollectionModel.Response;
using CommonLib.Models.Base;

namespace Catalog.Api.Models.CollectionModel.Request
{
    public class DeleteCollectionRequest : PagingSearchRequestBase
    {
        public Guid Id { get; set; }
    }
}
