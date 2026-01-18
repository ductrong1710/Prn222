using DataAccessLayer.Entities;

public class MessageService : IMessageService
{
    private readonly IUnitOfWork _uow;

    public MessageService(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public void Create(string content)
    {
        _uow.Messages.Add(new Message
        {
            Content = content,
            CreatedAt = DateTime.Now
        });

        _uow.Save();
    }

    public IEnumerable<Message> GetAll()
    {
        return _uow.Messages.GetAll();
    }
}
