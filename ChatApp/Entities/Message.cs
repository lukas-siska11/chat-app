namespace ChatApp.Entities;

public class Message : IEntity
{
    public Guid Id { get; set; }

    public string Content { get; set; } = string.Empty;

    public Guid ChatRoomId { get; set; }

    public Guid UserId { get; set; }

    public User? User { get; set; }
    
    public DateTimeOffset CreatedDate { get; set; } = DateTimeOffset.UtcNow;
}