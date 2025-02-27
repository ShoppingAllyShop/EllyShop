namespace Catalog.Api.Models.CollectionModel.Request
{
    public class CollectionRequest
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}
