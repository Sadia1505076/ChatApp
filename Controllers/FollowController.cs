using ChatApi.Context;
using ChatApi.Dtos;
using ChatApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace ChatApi.Controllers;

[ApiController]
[Route("[controller]")]
public class FollowController : ControllerBase
{
    private readonly ILogger<FollowController> _logger;
    private readonly ChatApiDbContext _context;

    public FollowController(ILogger<FollowController> logger, ChatApiDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet]
    [Route("GetAllFollowers")]
    public IEnumerable<User> GetAllFollowers(User currentUser)
    {
        return [.. _context.Follows.Where(f => f.Followee == currentUser).Select(f => f.Follower)]; // ToArray() has nullException possibility. this handles that issue gracefully
    }

    [HttpGet]
    [Route("GetAllFollowees")]
    public IEnumerable<User> GetAllFollowees(User currentUser)
    {
        return [.. _context.Follows.Where(f => f.Follower == currentUser).Select(f => f.Followee)];
    }

    [HttpGet]
    [Route("GetAllChats")]
    public IEnumerable<Follow> GetAllChats(User currentUser)
    {
        return [.. _context.Follows.Where(f => (f.Follower == currentUser || f.Followee == currentUser) && f.Conversation != null)];
    }

    [HttpGet]
    [Route("GetAllToBeChats")]
    public IEnumerable<Follow> GetAllToBeChats(User currentUser)
    {
        return [.. _context.Follows.Where(f => (f.Follower == currentUser || f.Followee == currentUser) && f.Conversation == null)];
    }

    [HttpPost]
    [Route("AddFollower")]
    public async Task<IActionResult> AddFollower(AddFollowerDto addFollowerDto)
    {
        Follow follow = new () {
            FolloweeId = addFollowerDto.Followee.Id,
            FollowerId = addFollowerDto.Follower.Id,
            FollowedOn = DateTime.UtcNow,
            Follower = addFollowerDto.Follower,
            Followee = addFollowerDto.Followee
        };
        await _context.Follows.AddAsync(follow);
        await _context.SaveChangesAsync();
        return Ok(follow);
    }
}
