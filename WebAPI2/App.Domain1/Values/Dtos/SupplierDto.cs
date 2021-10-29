using System.Collections.Generic;

namespace App.Domain.Dtos
{
    public class SupplierDto
    {
        public int SupplierId { get; set; }
        public string NameSupplier { get; set; }
        public string Address { get; set; }

        public virtual ICollection<ProductDto> Products { get; set; }
    }
}
