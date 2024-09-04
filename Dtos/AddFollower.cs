using ChatApi.Models;

namespace ChatApi.Dtos;

public class AddFollowerDto
{
    public required User Follower { get; set; }
    public required User Followee { get; set; }
}