using System.ComponentModel.DataAnnotations;

namespace ChatApi.Models;

public class Conversation
{
    [Key]
    public int Id { get; set; }

    // navigation property
    public required Follow Follow { get; set; }
    public required ICollection<Message> Messages { get; set; }
}