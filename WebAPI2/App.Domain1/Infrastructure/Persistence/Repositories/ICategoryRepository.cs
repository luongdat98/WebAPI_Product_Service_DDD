using App.Domain.Entities;
using System.Collections.Generic;

namespace App.Domain.Persistence.Repositories
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetCategorys();
    }
}
