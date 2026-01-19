namespace BusinessLogicLayer.Services;

public interface IFirebaseService
{
    Task SendNotificationAsync(string token, string title, string body);
    Task SendNotificationToTopicAsync(string topic, string title, string body);
}
