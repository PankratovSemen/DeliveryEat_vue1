using DeliveryEat_vue1.Server.DataBase.Contexts;
using DeliveryEat_vue1.Server.DataBase.FakePaymentsSystem;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace DeliveryEat_vue1.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaySystemController : ControllerBase
    {
        private FakePaymentsSystemContext _paymentsSystemContext;
        public PaySystemController(FakePaymentsSystemContext _paymentsSystemContext)
        {
            this._paymentsSystemContext = _paymentsSystemContext;
        }



        
    }
}
