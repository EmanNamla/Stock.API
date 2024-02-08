using Stock.Core.Entites.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Core.Specifications
{
    public class ItemsWithItemTypesandSupplierSpecification: BaseSpecifications<Item>
    {
        public ItemsWithItemTypesandSupplierSpecification()
        {
            Includes.Add(i => i.Supplier);
            Includes.Add(i => i.ItemType);
        }
    }
}
