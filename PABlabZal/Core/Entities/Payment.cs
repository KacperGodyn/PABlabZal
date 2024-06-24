namespace PABlabZalApi.Core.Entities
{
    public class Payment
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public int RentalId { get; set; }
        public Rental Rental { get; set; }
    }
}