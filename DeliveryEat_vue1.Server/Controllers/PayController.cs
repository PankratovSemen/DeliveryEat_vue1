using DeliveryEat_vue1.Server.DataBase.Basket;
using DeliveryEat_vue1.Server.DataBase.Contexts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryEat_vue1.Server.Controllers
{
    public class PayController : Controller
    {
        // GET: PayController
        private ApplicationContext _context;
        public PayController(ApplicationContext _context)
        {
            this._context = _context;
        }

        // POST: PayController/Create
        [HttpPost]
        [Route("PayFinish")]
        public IActionResult PayFinish(int basketId,string Status)
        {
            if (_context.Baskets.Any(x => x.Id == basketId))
            {
                var history = new PayData
                {
                    BasketId = basketId,
                    Status = Status
                };
                _context.Pay.Add(history);
                _context.SaveChanges();
                return Json("OK");
            }
            else
            {
                return StatusCode(404);
            }
        }


        [HttpPost]
        [Route("GetStatus")]
        public IActionResult GetStatus(int basketId)
        {
            if (_context.Baskets.Any(x => x.Id == basketId))
            {
                
                return Json(_context.Pay.Where(x => x.BasketId == basketId).LastOrDefault().Status);
            }
            else
            {
                return StatusCode(404);
            }
        }



    }
}
