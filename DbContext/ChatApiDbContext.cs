namespace ChatApi.Context;

using ChatApi.Models;
using Microsoft.EntityFrameworkCore;

public class ChatApiDbContext : DbContext
{
    public ChatApiDbContext(DbContextOptions<ChatApiDbContext> options)
        : base(options)
    {
    }

    // Adding entities as DbSet properties
    public DbSet<User> Users { get; set; }
    public DbSet<Follow> Follows { get; set; }
    public DbSet<Conversation> Conversations { get; set; }
    public DbSet<Message> Messages { get; set; }

    //OnModelCreating() method is used to configure the model using ModelBuilder Fluent API
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure User entity // not needed. the [key] annotation should work
        // modelBuilder.Entity<User>()
        //     .HasKey(u => u.Id);

        // one to one relation with follow and conversation
        modelBuilder.Entity<Follow>()
        .HasOne(f => f.Conversation)
        .WithOne(c => c.Follow)
        .HasForeignKey<Conversation>(c => c.Id)
        .IsRequired();

        // Configure Follow entity with composite key
        // modelBuilder.Entity<Follow>()
        //     .HasKey(f => new { f.FollowerId, f.FolloweeId });

        // Configure many-to-many relationship between User and Follow
        modelBuilder.Entity<Follow>()
            .HasOne(f => f.Follower)
            .WithMany(u => u.Following)
            .HasForeignKey(f => f.FollowerId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Follow>()
            .HasOne(f => f.Followee)
            .WithMany(u => u.Followers)
            .HasForeignKey(f => f.FolloweeId)
            .OnDelete(DeleteBehavior.Restrict);

        // one to many between conversation and messages
        modelBuilder.Entity<Conversation>()
            .HasMany(c => c.Messages)
            .WithOne(m => m.Conversation)
            .IsRequired(true)
            .OnDelete(DeleteBehavior.Cascade);

    }
}