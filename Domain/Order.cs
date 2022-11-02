using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Order : Entity
    {
        public int UserId { get; set; }
        public int ShopId { get; set; }

        public virtual User User { get; set; }
        public virtual Shop Shop { get; set; }
        public virtual ICollection<OrderLine> OrderLines { get; set; }
    }
}
