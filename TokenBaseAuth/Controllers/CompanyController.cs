using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TokenBaseAuth.Data;
using TokenBaseAuth.Entites;
using TokenBaseAuth.Repositories;

namespace TokenBaseAuth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : Repository<Company>
    {
        public CompanyController(AppDbContext context) : base(context)
        { }
        public bool DatabaseBusiness()
        {
            Add(new Company
            {
                Name = "GençAy A.Ş"
            });
            Add(new Company
            {
                Name = "NG A.Ş"
            });

            Add(new Product { CompanyId = 1, Name = "X", Quantity = 10 });
            Add(new Product { CompanyId = 1, Name = "Y", Quantity = 11 });
            Add(new Product { CompanyId = 1, Name = "Z", Quantity = 12 });

            Add(new Product { CompanyId = 2, Name = "A", Quantity = 13 });
            Add(new Product { CompanyId = 2, Name = "B", Quantity = 14 });

            Commit();

            return true;
        }
    }
}
