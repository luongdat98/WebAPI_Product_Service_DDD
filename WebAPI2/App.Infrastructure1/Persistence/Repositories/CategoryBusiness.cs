using System.Collections.Generic;
using System.Linq;
using App.Domain.Entities;
using App.Infrastructure.Persistence;
using App.Domain.Persistence.Repositories;

namespace App.Infrastructure.Repositories
{
    public class CategoryBusiness : ICategoryRepository
    {
        private readonly EcommerceContext _ecommerceDBContext;

        public CategoryBusiness(EcommerceContext ecommerceDBContext)
        {
            _ecommerceDBContext = ecommerceDBContext;
        }

        public IEnumerable<Category> GetCategorys()
        {
            var categories = _ecommerceDBContext.Categories.ToList().AsQueryable();
            return categories;
        }
    }
}
