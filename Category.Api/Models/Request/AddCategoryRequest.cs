namespace Category.Api.Models.Request
{
    public class AddCategoryRequest
    {
        public string Name { get; set; } = string.Empty;
        public int Level { get; set; }
        public Guid? ParentId { get; set; }
        public int CategoryLevel { get; set; }
        public bool? Gender { get; set; }
    }
}
