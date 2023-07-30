namespace Ecommerce.DTO.Models
{
    public class AddCategoryRequest
    {
        public long Parent_Id { get; set; }
        public string Name { get; set; }
    }
    public class CategoryResponse
    {
        public long Id { get; set; }
        public long? Parent_Id { get; set; }
        public string Name { get; set; }
    }
}
