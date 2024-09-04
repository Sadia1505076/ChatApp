using System.ComponentModel.DataAnnotations;

namespace ChatApi.Models;

public class Message
{
    [Key]
    public int Id { get; set; }
    public required string Body { get; set; }
    public required DateTime SentAt { get; set; }
    public DateTime? SeenAt { get; set; }

    // navigation property. they create FromId, ToId and ConversationId columns to create the foreign key relation
    public required User From { get; set; }
    public required User To { get; set; }
    public required Conversation Conversation { get; set; }
}