using App.Domain.Entities;
using App.Infrastructure.Persistence;
using App.Domain.Persistence.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace App.Infrastructure.Repositories
{
    public class SupplierBusiness : ISupplierRepository
    {
        private readonly EcommerceContext _ecommerceDBContext;

        public SupplierBusiness(EcommerceContext ecommerceDBContext)
        {
            _ecommerceDBContext = ecommerceDBContext;
        }

        public IEnumerable<Supplier> GetSuppliers()
        {
            var suppliers = _ecommerceDBContext.Suppliers.ToList().AsQueryable();
            return suppliers;
        }
    }
}
