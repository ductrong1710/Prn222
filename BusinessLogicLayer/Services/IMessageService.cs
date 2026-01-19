using BusinessLogicLayer.DTOs;

namespace BusinessLogicLayer.Services;

public interface IMessageService
{
    Task<MessageDto> CreateMessageAsync(CreateMessageDto dto);
    Task<List<MessageDto>> GetAllMessagesAsync();
    Task<MessageDto?> GetMessageByIdAsync(int id);
}
