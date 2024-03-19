using DeliveryEat_vue1.Server.DataBase;
using DeliveryEat_vue1.Server.DataBase.Contexts;
using DeliveryEat_vue1.Server.Interfaces.DataCode;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryEat_vue1.Server.Controllers

{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        ApplicationContext context;

        public ProductController(ApplicationContext context)
        {
            this.context = context;
        }
        
        // GET: ProductController
        [HttpGet(Name = "GetProduct")]
        public IEnumerable<Product> GetProduct()
        {
            
            return context.Product.ToArray();
        }

        // GET: ProductController/Details/5
        

        // POST: ProductController/Create
        [HttpPost]
        [Route("AddProduct")]
        [Authorize]
        public JsonResult AddProduct(string Title, string Preview, string Description, int Coast, string Visability)
        {
            Product product = new Product { Coast = Coast, Visability = Visability,Title=Title,Preview=Preview,Description=Description };
            context.Product.Add(product);
            context.SaveChanges();
            return Json("OK");
        }

        
    }
}
