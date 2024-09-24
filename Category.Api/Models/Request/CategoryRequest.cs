namespace Category.Api.Models.Request
{
    public class CategoryRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Guid? ParentId { get; set; }
        public int CategoryLevel { get; set; }
        public bool? Gender { get; set; }
    }
}
