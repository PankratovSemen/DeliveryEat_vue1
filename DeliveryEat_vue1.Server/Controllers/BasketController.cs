using DeliveryEat_vue1.Server.DataBase.Basket;
using DeliveryEat_vue1.Server.DataBase.Contexts;
using DeliveryEat_vue1.Server.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Net.WebSockets;

namespace DeliveryEat_vue1.Server.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class BasketController : Controller
    {
        private ILogger<Startup> _logger;
        private ApplicationContext _context;
        public BasketController(ApplicationContext context, ILogger<Startup> logger)
        {
            this._context = context;
            _logger = logger;
        }
        [HttpGet]
        [Route("api/GetBasket")]
        public JsonResult GetBasketProduct(string session)
        {
            SessionHeadndler sessions = new(_context);
            //if (sessions.GetSession(session) == null)
            //    return null;
            var list = Convert.ToInt32(sessions.GetSession(session).Result.Where(x => x.Name == "basket").LastOrDefault().Value);
            if (list == null)
                return Json("");
            List<Product> products = new List<Product>();

            var basketItem = _context.BasketItem.Where(x => x.BasketId == list).ToList();

            foreach (var t in basketItem)
            {

                var p = _context.Product.Where(x => x.Id == t.ProductId).FirstOrDefault();
                products.Add(p);
            }
            //foreach(var item in list)
            //{


            //}
            return Json(products);





        }

        [HttpGet]
        [Route("api/GetBuskLog")]
        public JsonResult GetBuskLog(int idUser)
        {
            return Json(_context.Baskets.Where(x => x.UserId == idUser));
        }

        [HttpPost]
        [Route("api/Add")]
        public string Add(int products, string? sessionId)
        {
            SessionHeadndler session = new SessionHeadndler(_context);
            if (sessionId != null)
            {
                int id = Convert.ToInt32(session.GetSession(sessionId).Result.Where(x => x.Name == "basket").LastOrDefault().Value);
                int Pid = _context.Product.Where(x => x.Id == products).FirstOrDefault().Id;
                BasketItem item = new BasketItem
                {
                    ProductId = Pid,
                    BasketId = id,
                };
                _context.BasketItem.Add(item);
                _context.SaveChanges();

                return sessionId;
            }
            var product = _context.Product.Where(x => x.Id == products).FirstOrDefault();
            var basket = new BasketData
            {
                Total = 0
            };
            _context.Baskets.Add(basket);
            _context.SaveChanges();
            int code = basket.Id;
            var basketID = new BasketItem
            {
                ProductId = product.Id,
                BasketId = code,
            };
            _context.BasketItem.Add(basketID);
            _context.SaveChanges();
            string ses = session.CreateSession("basket", code.ToString()).Result;
            return ses;



        }

        [HttpPost]
        [Route("api/GetCount")]
        public int GetCount(string session)
        {
            SessionHeadndler headndler = new(_context);
            int id = 0;
            if (session != null)
            {
                id = Convert.ToInt32(headndler.GetSession(session).Result.Where(x => x.Name == "basket").LastOrDefault().Value);

            }

            var productsCount = _context.BasketItem.GroupBy(d => new { d.ProductId, d.BasketId })
             .Where(g => g.Count() > 1).Select(g => new { g.Key.ProductId, g.Key.BasketId }).Where(x => x.BasketId == id).Count();
            return productsCount;

        }

        //Count product return
        [HttpPost]
        [Route("api/GetCount")]
        public int GetCountProduct(string session, int product)
        {
            SessionHeadndler headndler = new(_context);
            int id = 0;
            if (session != null)
            {
                id = Convert.ToInt32(headndler.GetSession(session).Result.Where(x => x.Name == "basket").LastOrDefault().Value);

            }
            else if (session == null)
            {
                return 0;
            }

            return _context.BasketItem.Where(x => x.ProductId == product && x.BasketId == id).Count();
        }

    }
}
