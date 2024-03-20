using DeliveryEat_vue1.Server.DataBase;
using DeliveryEat_vue1.Server.DataBase.Contexts;
using DeliveryEat_vue1.Server.Model;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryEat_vue1.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BasketController : Controller
    {
        private ApplicationContext _context;
        BasketController(ApplicationContext context) {
            this._context = context;
        }
        [HttpGet]
        [Route("api/GetBasket")]
        public JsonResult GetBasket(string session)
        {
            SessionHeadndler sessions = new();
            var list = sessions.GetSession(session);
            int bask = 0;
            foreach(var item in list)
            {
                if (item.Name == "basket")
                {
                    bask = Convert.ToInt32(item.Value);
                }
            }
            List<BasketData> baskets = _context.Baskets.Where(x => x.Id == bask).ToList();

            return Json(baskets);
        }
    }
}
