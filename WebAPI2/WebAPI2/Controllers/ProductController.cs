using App.Domain.Dtos;
using App.Domain.Persistence.Repositories;
using App.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using WebAPI2.Services.ProductSer;

namespace APIWeb.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly EcommerceContext _context;
        private IProductRepository _productRepository;
        private ProductService _productService;

        public ProductController(EcommerceContext context, IProductRepository productRepository,
                                ProductService productService)
        {
            _context = context;
            _productRepository = productRepository;
            _productService = productService;
        }

        [Route("search")]
        [HttpGet]
        public IActionResult GetAllProduct([FromQuery] ProductPaging productPaging, string searchProduct = null, int idCategory = 0)
        {
            if (idCategory > 0 && !String.IsNullOrEmpty(searchProduct))
            {
                var productFilterWithSearch = _productRepository.GetAllProductsBySearchAndFilter(productPaging, searchProduct, idCategory);

                var productData = _productService.GetDataPaginateProduct(productFilterWithSearch);

                Response.Headers.Add("Pagining", JsonConvert.SerializeObject(productData.Meta));
                return new JsonResult(productData);
            }
            else if (idCategory > 0 && String.IsNullOrEmpty(searchProduct))
            {
                var productFilterWithoutSearch = _productRepository.GetProductByFilterCategory(productPaging, idCategory);

                var productData = _productService.GetDataPaginateProduct(productFilterWithoutSearch);

                Response.Headers.Add("Pagining", JsonConvert.SerializeObject(productData.Meta));
                return new JsonResult(productData);
            }
            else if (idCategory == 0 && string.IsNullOrEmpty(searchProduct))
            {
                var products = _productRepository.GetAllProducts(productPaging);

                var productData = _productService.GetDataPaginateProduct(products);

                Response.Headers.Add("Pagining", JsonConvert.SerializeObject(productData.Meta));
                return new JsonResult(productData);
            }
            else if (idCategory == 0 && !string.IsNullOrEmpty(searchProduct))
            {
                var productSearch = _productRepository.GetAllProductsBySearch(productPaging, searchProduct);

                var productData = _productService.GetDataPaginateProduct(productSearch);

                Response.Headers.Add("Pagining", JsonConvert.SerializeObject(productData.Meta));
                return new JsonResult(productData);
            }
            else
            {
                return NotFound("Not Product");
            }
        }

        [Route("id")]
        [HttpGet]
        public IActionResult GetProductByIdProduct(int id)
        {
            var product = _productRepository.GetProductById(id);
            if (product != null)
            {
                return new JsonResult(product);
            }
            return NotFound($"Product with Id product: {id} was not found");
        }

        [Route("addproduct")]
        [HttpPost]
        public IActionResult AddProduct(ProductAddDto product)
        {
            var item = _productRepository.AddProduct(product);
            if (item != null)
            {
                return new JsonResult("Add product successful");
            }
            else
            {
                return NotFound($"Add Product: {item.NameProduct} is fail");
            }
        }

        //[Route("editproduct/{id}")]
        [HttpPut]
        public IActionResult EditProduct(ProductDto product)
        {
            var item = _productRepository.UpdateProduct(product);
            if (item != null)
            {
                return new JsonResult("Update product successful");
            }
            else
            {
                return NotFound($"Edit Product: {item.NameProduct} is fail");
            }
        }

        [Route("{id}")]
        [HttpDelete]
        public IActionResult DeleteProduct(int id)
        {
            if (!string.IsNullOrEmpty(id.ToString()))
            {
                _productRepository.DeleteProduct(id);
                return new JsonResult("Delete Successful");
            }
            return NotFound($"Delete Product: {id} is fail");
        }

        //[Route("search")]
        //[HttpGet]
        //public IActionResult GetAllProduct([FromQuery] ProductPaging productPaging, string searchProduct = null, int idCategory = 0)
        //{
        //    if (idCategory > 0 && !String.IsNullOrEmpty(searchProduct))
        //    {
        //        var productFilterWithSearch = _productRepository.GetAllProductsBySearchAndFilter(productPaging, searchProduct, idCategory);
        //        var metadata = new
        //        {
        //            productFilterWithSearch.TotalCount,
        //            productFilterWithSearch.PageSize,
        //            productFilterWithSearch.CurrentPage,
        //            productFilterWithSearch.HasNext,
        //            productFilterWithSearch.HasPrevious
        //        };
        //        ProductDataToClientDto productDataFilterWithSearch = new ProductDataToClientDto
        //        {
        //            Items = productFilterWithSearch,
        //            Meta = metadata
        //        };
        //        Response.Headers.Add("Pagining", JsonConvert.SerializeObject(metadata));
        //        return new JsonResult(productDataFilterWithSearch);
        //    }
        //    else if (idCategory > 0 && String.IsNullOrEmpty(searchProduct))
        //    {
        //        var productFilterWithoutSearch = _productRepository.GetProductByFilterCategory(productPaging, idCategory);
        //        var metadata1 = new
        //        {
        //            productFilterWithoutSearch.TotalCount,
        //            productFilterWithoutSearch.PageSize,
        //            productFilterWithoutSearch.CurrentPage,
        //            productFilterWithoutSearch.HasNext,
        //            productFilterWithoutSearch.HasPrevious
        //        };
        //        ProductDataToClientDto productDataFilterWithoutSearch = new ProductDataToClientDto
        //        {
        //            Items = productFilterWithoutSearch,
        //            Meta = metadata1
        //        };
        //        Response.Headers.Add("Pagining", JsonConvert.SerializeObject(metadata1));
        //        return new JsonResult(productDataFilterWithoutSearch);
        //    }
        //    else if (idCategory == 0 && string.IsNullOrEmpty(searchProduct))
        //    {
        //        var products = _productRepository.GetAllProducts(productPaging);
        //        var metadata2 = new
        //        {
        //            products.TotalCount,
        //            products.PageSize,
        //            products.CurrentPage,
        //            products.HasNext,
        //            products.HasPrevious
        //        };
        //        ProductDataToClientDto productDataPaging = new ProductDataToClientDto
        //        {
        //            Items = products,
        //            Meta = metadata2
        //        };

        //        Response.Headers.Add("Pagining", JsonConvert.SerializeObject(metadata2));
        //        return new JsonResult(productDataPaging);
        //    }
        //    else if (idCategory == 0 && !string.IsNullOrEmpty(searchProduct))
        //    {
        //        var productSearch = _productRepository.GetAllProductsBySearch(productPaging, searchProduct);

        //        var metadata3 = new
        //        {
        //            productSearch.TotalCount,
        //            productSearch.PageSize,
        //            productSearch.CurrentPage,
        //            productSearch.HasNext,
        //            productSearch.HasPrevious
        //        };

        //        ProductDataToClientDto productDataSearch = new ProductDataToClientDto
        //        {
        //            Items = productSearch,
        //            Meta = metadata3
        //        };
        //        Response.Headers.Add("Pagining", JsonConvert.SerializeObject(metadata3));
        //        return new JsonResult(productDataSearch);
        //    }
        //    else
        //    {
        //        return NotFound("Not Product");
        //    }
        //}
    }
}
