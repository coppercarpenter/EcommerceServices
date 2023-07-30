namespace Ecommerce.DTO.DbModels
{
    public class Seller
    {
        public Seller()
        {
            Products = new HashSet<Product>();
        }

        public long Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Image { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string CompanyAddress { get; set; }
        public string Fax { get; set; }
        public long City_Id { get; set; }


        public virtual City City { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}