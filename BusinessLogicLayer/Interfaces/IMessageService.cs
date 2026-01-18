using DataAccessLayer.Entities;

public interface IMessageService
{
    void Create(string content);
    IEnumerable<Message> GetAll();
}
