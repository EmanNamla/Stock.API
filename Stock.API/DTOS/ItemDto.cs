namespace Stock.API.DTOS
{
    public class ItemDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
        public string ImageName { get; set; }

        public DateTime ExpirationDate { get; set; }

        public double WholesalePrice { get; set; }

        public double Price { get; set; }
        public int ItemTypeId { get; set; }


        public int SupplierId { get; set; }

    }
}
