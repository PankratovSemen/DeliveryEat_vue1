using DeliveryEat_vue1.Server.DataBase;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using System;
using System.Text.Json;
using Newtonsoft.Json.Linq;
using System.Text.Json.Nodes;
using System.IdentityModel.Tokens.Jwt;
using System.Text.RegularExpressions;
using DeliveryEat_vue1.Server.DataBase.Contexts;
using Microsoft.EntityFrameworkCore;

namespace DeliveryEat_vue1.Server.Model
{
    public class SessionHeadndler
    {
        private ApplicationContext context;
        public SessionHeadndler(ApplicationContext context) 
        {
            this.context = context;
        }
        public async Task<string> CreateSession(string name, string value)
        {
            DateTime dateTime = DateTime.Now;
           
            
            
            string hash = DateTime.Now.ToString().GetHashCode().ToString("x");
            var session = new Sdata { ID=hash, Name= name, Value=value, dateEnd =  dateTime.AddDays(1) };
            await context.Sessions.AddAsync(session);
            await context.SaveChangesAsync();
            return hash;
            
        }

        public  async Task<List<Sdata>> GetSession(string session)
        {
            if (context.Sessions.Any(x => x.ID == session && x.dateEnd >= DateTime.Now)) { return await context.Sessions.Where(x => x.ID == session && x.dateEnd >= DateTime.Now).ToListAsync(); }
            return null;
          
        }
    }
}
