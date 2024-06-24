namespace PABlabZalApi.Core.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public ICollection<Rental> Rentals { get; set; }
    }
}