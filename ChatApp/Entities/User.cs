namespace ChatApp.Entities;

public class User : IEntity
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Surname { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public string AvatarColor { get; set; } = string.Empty;
}