namespace Social.Domain.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required decimal Price { get; set; }
        public required Client Client { get; set; }
        public required Guid ClientId {  get; set; }
    }
}
