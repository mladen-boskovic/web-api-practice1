using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Cart : Entity
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public bool IsCompleted { get; set; }

        public virtual User User { get; set; }
        public virtual Product Product { get; set; }
    }
}
