using App.Domain.Dtos;
using App.Domain.Entities;
using App.Domain.Helpers;

namespace App.Domain.Persistence.Repositories
{
    public interface IProductRepository
    {

        PageList<Product> GetAllProducts(ProductPaging productPaging);


        PageList<Product> GetAllProductsBySearch(ProductPaging productPaging, string search);

        ProductEditDto GetProductById(int idProduct);

        Product AddProduct(ProductAddDto product);

        Product UpdateProduct(ProductDto item);

        void DeleteProduct(int idProduct);

        PageList<Product> GetProductByFilterCategory(ProductPaging productPaging, int idCategory);

        PageList<Product> GetAllProductsBySearchAndFilter(ProductPaging productPaging, string search, int idCategory = 0);

    }
}
