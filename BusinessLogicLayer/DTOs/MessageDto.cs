namespace BusinessLogicLayer.DTOs;

public class MessageDto
{
    public int Id { get; set; }
    public string User { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}

public class CreateMessageDto
{
    public string User { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
}
