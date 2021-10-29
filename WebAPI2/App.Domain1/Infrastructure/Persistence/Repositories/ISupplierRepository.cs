using System.Collections.Generic;
using App.Domain.Entities;

namespace App.Domain.Persistence.Repositories
{
    public interface ISupplierRepository
    {
        IEnumerable<Supplier> GetSuppliers();
    }
}
