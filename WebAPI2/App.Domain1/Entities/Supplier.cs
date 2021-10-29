using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Domain.Entities
{
    public class Supplier
    {
        public int SupplierId { get; set; }
        public string NameSupplier { get; set; }
        public string Address { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
