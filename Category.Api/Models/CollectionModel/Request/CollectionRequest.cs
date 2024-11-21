namespace Catalog.Api.Models.CollectionModel.Request
{
    public class CollectionRequest
    {
        public Guid Id { get; set; }
        public string? Name { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}
