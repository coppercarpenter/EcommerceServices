namespace Ecommerce.DTO.DbModels
{
    public class Customer
    {
        public Customer()
        {
            CartProducts = new HashSet<CartProduct>();
            Orders = new HashSet<Order>();
        }

        public long Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<CartProduct> CartProducts { get; set; }
    }
}