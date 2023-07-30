namespace Ecommerce.DTO.DbModels
{
    public class Category
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }

        public long Id { get; set; }
        public long? Parent_Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public bool IsDeleted { get; set; }


        public virtual ICollection<Product> Products { get; set; }
    }
}