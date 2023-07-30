namespace Ecommerce.DTO.DbModels
{
    public class City
    {
        public City()
        {
            Sellers = new HashSet<Seller>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public long Country_Id { get; set; }
        public virtual Country Country { get; set; }

        public virtual ICollection<Seller> Sellers { get; set; }
    }
}