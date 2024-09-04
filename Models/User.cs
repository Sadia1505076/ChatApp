using System.ComponentModel.DataAnnotations;

namespace ChatApi.Models;

public class User
{
    [Key]
    public int Id { get; set; }
    public required string Name { get; set; }
    public DateTime? CreatedAt { get; set; }

    // Navigation property for the many-to-many relationship
    public ICollection<Follow>? Followers { get; set; }
    public ICollection<Follow>? Following { get; set; }
}