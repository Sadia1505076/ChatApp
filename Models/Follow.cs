using System.ComponentModel.DataAnnotations;

namespace ChatApi.Models;

public class Follow
{
    [Key]
    public int Id { get; set; }
    public required int FollowerId { get; set; }
    public required int FolloweeId { get; set; }
    public required DateTime FollowedOn { get; set; }

    // Navigation properties
    public required User Follower { get; set; }
    public required User Followee { get; set; }

    // Even if we make conversation required, this doesn't create ConversationId column because Follow and Conversation has one to one relation
    // so basically the Id for this table is same as the Id of Conversation table
    public Conversation? Conversation { get; set; }
}