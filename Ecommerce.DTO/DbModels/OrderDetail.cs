namespace Ecommerce.DTO.DbModels
{
    public class OrderDetail
    {
        public long Id { get; set; }
        public long Order_Id { get; set; }
        public decimal Price { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public long UnitOfMeasure_Id { get; set; }
        public long Currency_Id { get; set; }
        public decimal PerUnitPrice { get; set; }

        public virtual Order Order { get; set; }
        public virtual Currency Currency { get; set; }
        public virtual UOM UnitOfMeasure { get; set; }

    }
}
