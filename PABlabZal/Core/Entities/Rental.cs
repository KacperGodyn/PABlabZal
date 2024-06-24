namespace PABlabZalApi.Core.Entities
{
    public class Rental
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int ClientId { get; set; }
        public Client Client { get; set; }
        public int CarId { get; set; }
        public Car Car { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public ICollection<Payment> Payments { get; set; }
    }
}
