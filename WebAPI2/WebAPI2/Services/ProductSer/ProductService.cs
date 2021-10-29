using App.Domain.Helpers;
using App.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Domain.Dtos;

namespace WebAPI2.Services.ProductSer
{
    public class ProductService
    {
        public ProductDataToClientDto GetDataPaginateProduct(PageList<Product> product)
        {
            var metadata = new
            {
                product.TotalCount,
                product.PageSize,
                product.CurrentPage,
                product.HasNext,
                product.HasPrevious
            };
            ProductDataToClientDto productDataFilterWithoutSearch = new ProductDataToClientDto
            {
                Items = product,
                Meta = metadata
            };
            return productDataFilterWithoutSearch;
        }
    }
}
