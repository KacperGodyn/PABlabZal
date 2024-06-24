namespace PABlabZalApi.Core.Entities
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int PhoneNumber { get; set; }
        public ICollection<Rental> Rentals { get; set; }
    }
}