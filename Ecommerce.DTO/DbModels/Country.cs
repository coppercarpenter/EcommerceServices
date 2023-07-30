namespace Ecommerce.DTO.DbModels
{
    public class Country
    {
        public Country()
        {
            Cities = new HashSet<City>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string Flag { get; set; }

        public virtual ICollection<City> Cities { get; set; }
    }
}