namespace DataAccessLayer.Entities;

public class Message
{
    public int Id { get; set; }
    public string User { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}
