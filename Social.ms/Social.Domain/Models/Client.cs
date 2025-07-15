using System.ComponentModel.DataAnnotations;

namespace Social.Domain.Models
{
    public class Client
    {
        [Key]
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
