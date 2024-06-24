namespace PABlabZalApi.Core.Entities
{
    public class Car
    {
        public int Id { get; set; }
        public string MadeBy { get; set; }
        public string Model { get; set; }
        public string LicensePlate { get; set; }
        public decimal PricePerDay { get; set; }
        public ICollection<Rental> Rentals { get; set; }
    }
}