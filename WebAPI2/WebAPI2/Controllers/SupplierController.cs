using App.Domain.Persistence.Repositories;
using App.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly EcommerceContext _context;
        private ISupplierRepository _supplierRepository;

        public SupplierController(EcommerceContext context, ISupplierRepository supplierRepository)
        {
            _context = context;
            _supplierRepository = supplierRepository;
        }

        [HttpGet]
        public IActionResult GetSuppliers()
        {
            var suppliers = _supplierRepository.GetSuppliers();
            if (suppliers != null)
            {
                return new JsonResult(suppliers);
            }
            return NotFound("Not Suppliers");
        }
    }
}
