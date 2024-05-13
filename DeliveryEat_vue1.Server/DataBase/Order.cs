namespace DeliveryEat_vue1.Server.DataBase
{
    public class Order
    {
        public int Id { get; set; }
        public int BasketId { get; set; }
       public string Surname { get; set; }
       public string Name { get; set; }
       public string MiddleName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string? Comments { get; set; }
    }
}
