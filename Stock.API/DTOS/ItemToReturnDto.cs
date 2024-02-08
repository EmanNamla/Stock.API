using Stock.Core.Entites.Items;
using Stock.Core.Entites;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stock.API.DTOS
{
    public class ItemToReturnDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
        public IFormFile Image { get; set; }
        public string? ImageName { get; set; }

        public DateTime ExpirationDate { get; set; }

        public double WholesalePrice { get; set; }

        public double Price { get; set; }
        public int ItemTypeId { get; set; }


        public int SupplierId { get; set; }

    }
}
