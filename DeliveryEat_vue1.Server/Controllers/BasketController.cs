using DeliveryEat_vue1.Server.DataBase.Basket;
using DeliveryEat_vue1.Server.DataBase.Contexts;
using DeliveryEat_vue1.Server.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Security.Cryptography;

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
        public JsonResult GetBasketProduct(string? session,string?username, int basketid)
        {
            try
            {
                if(session!=null)
                {
                    SessionHeadndler sessions = new(_context);
                    //if (sessions.GetSession(session) == null)
                    //    return null;
                    int list = 0;
                    list = Convert.ToInt32(sessions.GetSession(session).Result.Where(x => x.Name == "basket").LastOrDefault().Value);
                    if (list == 0)
                        return Json("");
                    List<Product> products = new List<Product>();

                    var basketItem = _context.BasketItem.GroupBy(d => new { d.ProductId, d.BasketId })
                     .Where(g => g.Count() > 1 || g.Count() == 1).Select(g => new { g.Key.ProductId, g.Key.BasketId }).Where(x => x.BasketId == list).ToList();

                    foreach (var t in basketItem)
                    {

                        var p = _context.Product.Where(x => x.Id == t.ProductId).FirstOrDefault();
                        if (!products.Contains(p))
                        {
                            products.Add(p);
                        }

                    }
                    //foreach(var item in list)
                    //{


                    //}
                    return Json(products);

                }
                else if (username!=null)
                {
                    int userId = _context.Users.Where(x => x.Login == username).FirstOrDefault().Id;
                    int basketId = _context.UserBasket.OrderBy(x=>x.Id).LastOrDefault(x => x.UserId == userId).BasketId;
                    List<Product> products = new List<Product>();

                    var basketItem = _context.BasketItem.GroupBy(d => new { d.ProductId, d.BasketId })
                     .Where(g => g.Count() > 1 || g.Count() == 1).Select(g => new { g.Key.ProductId, g.Key.BasketId }).Where(x => x.BasketId == basketId).ToList();

                    foreach (var t in basketItem)
                    {

                        var p = _context.Product.Where(x => x.Id == t.ProductId).FirstOrDefault();
                        if (!products.Contains(p))
                        {
                            products.Add(p);
                        }
                       

                    }
                   

                    return Json(products);
                }
                else if (basketid > 0)
                {
                    List<Product> products = new List<Product>();

                    var basketItem = _context.BasketItem.GroupBy(d => new { d.ProductId, d.BasketId })
                     .Where(g => g.Count() > 1 || g.Count() == 1).Select(g => new { g.Key.ProductId, g.Key.BasketId }).Where(x => x.BasketId == basketid).ToList();

                    foreach (var t in basketItem)
                    {

                        var p = _context.Product.Where(x => x.Id == t.ProductId).FirstOrDefault();
                        if (!products.Contains(p))
                        {
                            products.Add(p);
                        }

                    }

                    return Json(products);
                }

                return Json("");

            }
            catch(Exception e)
            {
                _logger.LogError(e.Message);
                return Json("Error");
            }



        }

     
        

        [HttpPost]
        [Route("api/Add")]
        public string Add(int products, string? sessionId,int? count,string? username)
        {
            try
            {
                SessionHeadndler session = new SessionHeadndler(_context);
                if (sessionId != null)
                {
                    int id = Convert.ToInt32(session.GetSession(sessionId).Result.Where(x => x.Name == "basket").LastOrDefault().Value);
                    int Pid = _context.Product.Where(x => x.Id == products).FirstOrDefault().Id;
                    if (_context.Pay.Any(x => x.BasketId == id))
                    {
                        var baskets = new BasketData
                        {
                            Total = 0
                        };
                        _context.Baskets.Add(baskets);
                        _context.SaveChanges();
                        int codes = baskets.Id;
                        var sessiondata = _context.Sessions.Where(x => x.ID == sessionId && x.Name == "basket").FirstOrDefault();
                        sessiondata.Value = codes.ToString();
                        _context.Sessions.Update(sessiondata);
                        _context.SaveChanges();
                    }
                    if (count > 0)
                    {
                        List<BasketItem> BI = new();
                        for (int i = 0; i < count; i++)
                        {
                            AddProductSession(Pid, id);
                           
                        }
                        
                        
                       

                    }
                    
                    _context.SaveChanges();

                    return sessionId;
                }
                else if (username != null)
                {
                    int usrId = _context.Users.Where(x => x.Login == username).FirstOrDefault().Id;
                    int bid = 0;
                    if(_context.UserBasket.Any(x => x.UserId == usrId))
                    {
                        bid = _context.UserBasket.OrderBy(x => x.Id).LastOrDefault(x => x.UserId == usrId).BasketId;
                    }
                    if (!_context.UserBasket.Any(x=>x.UserId==usrId) || _context.Pay.Any(x => x.BasketId == bid))
                    {
                        _logger.LogError("qffqf");
                        var baskets = new BasketData
                        {
                            Total = 0
                        };
                        _context.Baskets.Add(baskets);
                        _context.SaveChanges();
                        int codes = baskets.Id;
                        var UserBasket = new UserBasket
                        {
                            UserId = usrId,
                            BasketId = codes
                        };
                        _context.UserBasket.Add(UserBasket);
                        _context.SaveChanges();
                    }

                    _logger.LogError("testsingffwwgw");
                    int id = _context.UserBasket.OrderBy(x=>x.Id).LastOrDefault(x=>x.UserId==usrId).BasketId;
                    
                    int Pid = _context.Product.Where(x => x.Id == products).FirstOrDefault().Id;

                    if (count > 0)
                    {
                        List<BasketItem> BI = new();
                        for (int i = 0; i < count; i++)
                        {
                            AddProductSession(Pid, id);

                        }




                    }

                    _context.SaveChanges();

                    return "OK";

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
                if (count != null)
                {
                    for (int i = 0; i < count; i++)
                    {
                        _context.BasketItem.Add(basketID);

                    }
                    _context.SaveChanges();
                }
                else
                {
                    _context.BasketItem.Add(basketID);
                    _context.SaveChanges();
                }
                string ses = session.CreateSession("basket", code.ToString()).Result;
                return ses;



            }
            catch(Exception e)
            {
                _logger.LogError(e.Message);
                return null;
            }
        }

        [HttpGet]
        [Route("api/GetCount")]
        public int GetCount(string? session,string? username)
        {
            try
            {
                SessionHeadndler headndler = new(_context);
                int id = 0;
                if (session != null)
                {
                    id = Convert.ToInt32(headndler.GetSession(session).Result.Where(x => x.Name == "basket").LastOrDefault().Value);

                }
                else if (username != null)
                {
                    var user = _context.Users.Where(x=>x.Login==username).FirstOrDefault();
                    id = _context.UserBasket.OrderBy(x => x.Id).LastOrDefault(x => x.UserId == user.Id).BasketId;
                }
               

                var productsCount = _context.BasketItem.GroupBy(d => new { d.ProductId, d.BasketId })
                 .Where(g => g.Count() > 1 || g.Count() == 1).Select(g => new { g.Key.ProductId, g.Key.BasketId }).Where(x => x.BasketId == id).Count();
                
                return productsCount;
            }
            catch(Exception e)
            {
                _logger.LogError(e.Message);
                return 0;
            }

        }

        //Count product return
        [HttpGet]
        [Route("api/GetCountProduct")]
        public int GetCountProduct(string? session, int product,string? username,int basketid)
        {
            try
            {
                SessionHeadndler headndler = new(_context);
                int id = 0;
                if (session != null)
                {
                    id = Convert.ToInt32(headndler.GetSession(session).Result.Where(x => x.Name == "basket").LastOrDefault().Value);

                }
                else if (username != null)
                {
                    int userid = _context.Users.Where(x => x.Login == username).FirstOrDefault().Id;
                    id = Convert.ToInt32(_context.UserBasket.OrderBy(x=>x.Id).LastOrDefault(x => x.UserId == userid).BasketId);
                }
                else if (basketid != null)
                {
                    id = basketid;
                }

                return _context.BasketItem.Where(x => x.ProductId == product && x.BasketId == id).Count();
            }
            catch(Exception e)
            {
                _logger.LogError(e.Message);
                return 0;
            }
        }

        [HttpGet]
        [Route("api/GetProductBasket")]
        public JsonResult GetProductBasket(string session)
        {
            try
            {
                SessionHeadndler headndler = new(_context);
                int id = 0;
                if (session != null)
                {
                    id = Convert.ToInt32(headndler.GetSession(session).Result.Where(x => x.Name == "basket").LastOrDefault().Value);

                }
                List<Product> products = new List<Product>();
                var item = _context.BasketItem.GroupBy(d => new { d.ProductId, d.BasketId })
                 .Where(g => g.Count() > 1).Select(g => new { g.Key.ProductId, g.Key.BasketId }).Where(x => x.BasketId == id);
                foreach (var s in item)
                {
                    products.Add(_context.Product.Where(x => x.Id == s.ProductId).FirstOrDefault());
                }
                return Json(products);
            }
            catch (Exception e)
            {
                return Json(0);
            }
        }
        [HttpGet]
        [Route("api/ProductTotal")]
        public int ProductTotalCoast(string session, int product)
        {
            try
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

                int count = _context.BasketItem.Where(x => x.ProductId == product && x.BasketId == id).Count();
                int coast = _context.Product.Where(x => x.Id == product).FirstOrDefault().Coast;
                int Total = count * coast;
                return Total;
            }
            catch (Exception e)
            {
                return 0;
            }

        }

        [HttpGet]
        [Route("api/BasketTotal")]
        public int BasketTotal(string session)
        {
            try
            {
                SessionHeadndler headndler = new(_context);
                int id = 0;
                if (session != null)
                {
                    id = Convert.ToInt32(headndler.GetSession(session).Result.Where(x => x.Name == "basket").LastOrDefault().Value);

                }
                List<Product> products = new List<Product>();
                var item = _context.BasketItem.GroupBy(d => new { d.ProductId, d.BasketId })
                 .Where(g => g.Count() > 1).Select(g => new { g.Key.ProductId, g.Key.BasketId }).Where(x => x.BasketId == id);
                List<int> Totals = new();
                int CoastTotal = 0;
                foreach (var s in item)
                {
                    products.Add(_context.Product.Where(x => x.Id == s.ProductId).FirstOrDefault());
                    _logger.LogInformation(s.ProductId.ToString());
                    var t = _context.Product.Where(x => x.Id == s.ProductId).FirstOrDefault();
                    _logger.LogInformation(t.Id.ToString());
                    int count = _context.BasketItem.Where(x => x.ProductId == t.Id && x.BasketId == id).Count();
                    _logger.LogInformation(count.ToString());
                    int Total = t.Coast * count;
                    _logger.LogInformation(Total.ToString());
                    Totals.Add(Total);
                }



                foreach (var s in Totals)
                {
                    CoastTotal += s;

                }


                return CoastTotal;
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        private void AddProductSession(int PD,int BID)
        {
            _logger.LogError("Start");
            BasketItem item = new BasketItem
            {
                ProductId = PD,
                BasketId = BID,
            };

            _context.BasketItem.Add(item);
            _context.SaveChanges();
            _logger.LogError("End");
            _logger.LogError(BID.ToString());
        }



    }
}

