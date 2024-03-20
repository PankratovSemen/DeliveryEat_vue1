using DeliveryEat_vue1.Server.DataBase;
using System.Text.Json;

namespace DeliveryEat_vue1.Server.Model
{
    public class SessionHeadndler
    {
        public async Task<string> CreateSession(string name, string value)
        {
            DateTime dateTime = DateTime.Now;
            dateTime.AddDays(1);

            using (FileStream fs = new FileStream("sessions.json", FileMode.OpenOrCreate))
            {
                var session = new Sdata { ID=DateTime.Now.ToString().GetHashCode().ToString("x"), Name= name, Value=value, dateEnd = dateTime };
                await JsonSerializer.SerializeAsync<Sdata>(fs, session);
                return "Succefull";
            }
        }

        public List<Sdata> GetSession(string session)
        {
            using (FileStream fs = new FileStream("user.json", FileMode.OpenOrCreate))
            {
                var list = JsonSerializer.Deserialize<List<Sdata>>(fs);

                List<Sdata> result = list.Where(x => x.ID == session && x.dateEnd <= DateTime.Now).ToList();
                
                return result;
            }
        }
    }
}
