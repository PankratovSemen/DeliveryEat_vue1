namespace DeliveryEat_vue1.Server.DataBase.FakePaymentsSystem
{
    public class TransactionsHistory
    {
        public string Id { get; set; }
        public string OwnerId { get; set; }
        public string TypeOperation { get;set; }
        public string? Args { get; set; }
        public int Count { get; set; }
        public string Status { get; set; }
    }
}
