using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Product : Entity
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int QuantityAvailable { get; set; }
        public int ShopId { get; set; }

        public virtual Shop Shop { get; set; }
        public virtual ICollection<OrderLine> OrderLines { get; set; }
        public virtual ICollection<Cart> Carts { get; set; }
    }
}
