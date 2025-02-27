namespace Catalog.Api.Models.CategoryModel.Response
{
    public class CategoryResponse
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public Guid? ParentId { get; set; }
        public int? CategoryLevel { get; set; }
        public bool? Gender { get; set; }
    }
}
