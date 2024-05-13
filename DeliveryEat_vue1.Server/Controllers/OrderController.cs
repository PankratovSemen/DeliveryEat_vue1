using DeliveryEat_vue1.Server.DataBase;
using DeliveryEat_vue1.Server.DataBase.Basket;
using DeliveryEat_vue1.Server.DataBase.Contexts;
using DeliveryEat_vue1.Server.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DeliveryEat_vue1.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private ApplicationContext _context;
     
        public OrderController(ApplicationContext context)
        {
            _context = context;
            
        }
        [HttpPost]
        [Route("Create")]
        public IActionResult CreateUser(string Surname,string Name,string MiddleName,string Phone,string Address, string? Comments,int basketid)
        {



            if (!_context.Pay.Any(z => z.BasketId == basketid))
            {
                var Order = new Order
                {
                    BasketId = basketid,
                    Surname = Surname,
                    Name = Name,
                    MiddleName = MiddleName,
                    Phone = Phone,
                    Address = Address,
                    Comments = Comments,

                };
                var status = new PayData
                {
                    BasketId = basketid,
                    Status = "Обработан"
                };
                _context.Pay.Add(status);
                
                
                _context.SaveChanges();
                _context.Order.Add(Order);
                _context.SaveChanges();
                return new JsonResult("Ok");
            }
            return BadRequest();
        }

        [HttpGet]
        [Route("Get")]
        public IActionResult Get()
        {
            
            var order = from Order in _context.Order
                        join basket in _context.Baskets on Order.BasketId equals basket.Id
                        join basketitem in _context.BasketItem.GroupBy(d => new { d.ProductId, d.BasketId })
                     .Select(g => new { g.Key.ProductId, g.Key.BasketId }) on basket.Id equals basketitem.BasketId
                        join status in _context.Pay on basket.Id equals status.BasketId
                        join product in _context.Product on basketitem.ProductId equals product.Id
                        



                        select new
                        {
                            Id = Order.Id,
                            Product = product.Title,
                            ProductId = product.Id,
                            Address = Order.Address,
                            Phone = Order.Phone,
                            SNM = Order.Surname + " " + Order.Name + " " + Order.MiddleName,
                            Status = status.Status,
                            BasketId = basket.Id,
                            Comments = Order.Comments,
                           
                            

                        };

            




            return new JsonResult(order);

        }
        
        

        //[HttpGet]
        //[Route("GetProducts")]
        //public IActionResult GetProducts()
        //{
        //    var order = from Order in _context.Order
        //                join basket in _context.Baskets on Order.BasketId equals basket.Id
        //                join basketitem in _context.BasketItem on basket.Id equals basketitem.BasketId
        //                join adress in _context.Addresses on Order.AddressId equals adress.Id
        //                join user in _context.Users on Order.UserId equals user.Id
        //                join product in _context.Product on basketitem.ProductId equals product.Id


        //                select new
        //                {
        //                    Id = Order.Id,
        //                    Address = adress.City + " " + adress.Street + " " + adress.House,
        //                    Phone = user.Phone,
        //                    SNM = user.Surname + " " + user.Name + " " + user.MiddleName,
        //                    Email = user.Email,
        //                    Comments = Order.Comments,


        //                };






        //    return new JsonResult(order);

        //}

        [HttpPost]
        [Route("Address")]
        public IActionResult Address(string City,string Street,string House,string flat)
        {
            var Address = new Address
            {
                City = City,
                Street = Street,
                House = House,
                Flate = flat
            };
            _context.Addresses.Add(Address);
            _context.SaveChanges();

            return new JsonResult("OK");
        }

        private int Total(int basketId)
        {
            var productId = _context.BasketItem.Where(x => x.BasketId == basketId);
            int total = 0;

            foreach (var item in productId)
            {

                total += _context.Product.Where(x => x.Id == item.ProductId).FirstOrDefault().Coast;
            }
            return total;
        }


        [HttpPost]
        [Route("Cancel")]
        public IActionResult CancelOrder(int basketId)
        {
            var paystatus = _context.Pay.Where(x => x.BasketId == basketId).FirstOrDefault();
            paystatus.Status = "Отменен";
            _context.Pay.Update(paystatus);
            _context.SaveChanges();
            return Ok();
        }


        [HttpPost]
        [Route("Cooking")]
        public IActionResult CookinglOrder(int basketId)
        {
            var paystatus = _context.Pay.Where(x => x.BasketId == basketId).FirstOrDefault();
            paystatus.Status = "Заказ готов";
            _context.Pay.Update(paystatus);
            _context.SaveChanges();
            return Ok();
        }


        [HttpPost]
        [Route("Courier")]
        public IActionResult CourierlOrder(int basketId, int userid)
        {
            var delivery = new Delivery
            {
                BasketId = basketId,
                UserId = userid
            };
            _context.Delivery.Add(delivery);
            _context.SaveChanges();
            var paystatus = _context.Pay.Where(x => x.BasketId == basketId).FirstOrDefault();
            paystatus.Status = "Передано курьеру";
            _context.Pay.Update(paystatus);
            _context.SaveChanges();
            return Ok();
        }

        [HttpPost]
        [Route("Done")]
        public IActionResult DoneOrder(int basketId)
        {
            var paystatus = _context.Pay.Where(x => x.BasketId == basketId).FirstOrDefault();
            paystatus.Status = "Доставлен заказчику";
            _context.Pay.Update(paystatus);
            _context.SaveChanges();
            return Ok();
        }


        [HttpGet]
        [Route("GetOrderCourier")]
        public IActionResult GetOrderCourier(string username)
        {
            var user = _context.Users.Where(x => x.Login == username).FirstOrDefault();
            var orders = from delivery in _context.Delivery.Where(x => x.UserId == user.Id)
                         join order in _context.Order on delivery.BasketId equals order.BasketId
                         join status in _context.Pay on order.BasketId equals status.BasketId
                         select new
                         {
                             Id = order.Id,
                             SNM = order.Surname + " " + order.Name + " " + order.MiddleName,
                             Address = order.Address,
                             Phone = order.Phone,
                             Comment = order.Comments,
                             basketid = order.BasketId,
                             Status = status.Status
                         };

            return new JsonResult(orders);
        }



    }
}
