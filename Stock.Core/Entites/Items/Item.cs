using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Core.Entites.Items
{
    public class Item:BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        [NotMapped]
        public IFormFile Image { get; set; }

        public string ImageName { get; set; }

        public DateTime ExpirationDate { get; set; }

        public double WholesalePrice { get; set; }

        public double Price { get; set; }

        public int ItemTypeId { get; set; }
        public ItemType ItemType { get; set; }

        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; }
    }
}
