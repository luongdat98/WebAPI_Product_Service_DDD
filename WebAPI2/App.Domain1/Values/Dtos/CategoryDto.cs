using System.Collections.Generic;


namespace App.Domain.Dtos
{
    public class CategoryDto
    {
        public int CategoryId { get; set; }
        public string NameCategory { get; set; }

        public virtual ICollection<ProductDto> Products { get; set; }
    }
}
