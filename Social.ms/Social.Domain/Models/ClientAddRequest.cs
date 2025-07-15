using System.ComponentModel.DataAnnotations;

namespace Social.Domain.Models
{
    public class ClientAddRequest
    {
        [Key]
        public required Guid Id { get; set; }
        public required string Name { get; set; }
    }
}
