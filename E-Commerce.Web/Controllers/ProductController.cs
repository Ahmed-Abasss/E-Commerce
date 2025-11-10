using E_Commerce.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        [HttpGet(template:"{id}")]

        public ActionResult<Product> Get(int id)
        {
            return new Product() { id = id  , Name="test"};
        }


        [HttpGet]

        public ActionResult<IEnumerable<Product>> GetAll()
        {
            return new List<Product>()
            {
                new Product() { id = 1 , Name="test" },
                new Product() { id = 2 , Name="test2" },
                new Product() { id = 3 , Name="test3" },
            };
        }
    }
}
