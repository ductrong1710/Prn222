using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using Microsoft.Extensions.Configuration;

namespace BusinessLogicLayer.Services;

public class FirebaseService : IFirebaseService
{
    private readonly FirebaseMessaging _messaging;

    public FirebaseService(IConfiguration configuration)
    {
        if (FirebaseApp.DefaultInstance == null)
        {
            var credentialPath = configuration["Firebase:CredentialPath"];
            
            if (!string.IsNullOrEmpty(credentialPath) && File.Exists(credentialPath))
            {
                FirebaseApp.Create(new AppOptions
                {
                    Credential = GoogleCredential.FromFile(credentialPath)
                });
            }
            else
            {
                // Fallback: use environment variable or skip initialization
                var projectId = configuration["Firebase:ProjectId"];
                if (!string.IsNullOrEmpty(projectId))
                {
                    FirebaseApp.Create(new AppOptions
                    {
                        Credential = GoogleCredential.GetApplicationDefault(),
                        ProjectId = projectId
                    });
                }
            }
        }

        _messaging = FirebaseMessaging.DefaultInstance;
    }

    public async Task SendNotificationAsync(string token, string title, string body)
    {
        if (_messaging == null) return;

        var message = new Message
        {
            Token = token,
            Notification = new Notification
            {
                Title = title,
                Body = body
            },
            Data = new Dictionary<string, string>
            {
                { "timestamp", DateTime.UtcNow.ToString("o") }
            }
        };

        await _messaging.SendAsync(message);
    }

    public async Task SendNotificationToTopicAsync(string topic, string title, string body)
    {
        if (_messaging == null) return;

        var message = new Message
        {
            Topic = topic,
            Notification = new Notification
            {
                Title = title,
                Body = body
            },
            Data = new Dictionary<string, string>
            {
                { "timestamp", DateTime.UtcNow.ToString("o") }
            }
        };

        await _messaging.SendAsync(message);
    }
}
