using BusinessLogicLayer.DTOs;
using DataAccessLayer.Entities;
using DataAccessLayer.UnitOfWork;

namespace BusinessLogicLayer.Services;

public class MessageService : IMessageService
{
    private readonly IUnitOfWork _unitOfWork;
    
    // Múi gi? Vi?t Nam (UTC+7)
    private static readonly TimeZoneInfo VietnamTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");

    public MessageService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<MessageDto> CreateMessageAsync(CreateMessageDto dto)
    {
        var message = new Message
        {
            User = dto.User,
            Content = dto.Content,
            CreatedAt = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, VietnamTimeZone)
        };

        await _unitOfWork.Messages.AddAsync(message);
        await _unitOfWork.SaveChangesAsync();

        return MapToDto(message);
    }

    public async Task<List<MessageDto>> GetAllMessagesAsync()
    {
        var messages = await _unitOfWork.Messages.GetAllAsync();
        return messages.Select(MapToDto).ToList();
    }

    public async Task<MessageDto?> GetMessageByIdAsync(int id)
    {
        var message = await _unitOfWork.Messages.GetByIdAsync(id);
        return message != null ? MapToDto(message) : null;
    }

    private static MessageDto MapToDto(Message message)
    {
        return new MessageDto
        {
            Id = message.Id,
            User = message.User,
            Content = message.Content,
            CreatedAt = message.CreatedAt
        };
    }
}
