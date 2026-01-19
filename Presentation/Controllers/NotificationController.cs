using Microsoft.AspNetCore.Mvc;
using BusinessLogicLayer.Services;

namespace Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NotificationController : ControllerBase
{
    private readonly IFirebaseService _firebaseService;
    private readonly ILogger<NotificationController> _logger;

    public NotificationController(IFirebaseService firebaseService, ILogger<NotificationController> logger)
    {
        _firebaseService = firebaseService;
        _logger = logger;
    }

    /// <summary>
    /// G?i notification ??n m?t device c? th?
    /// </summary>
    [HttpPost("send-to-device")]
    public async Task<IActionResult> SendToDevice([FromBody] SendNotificationRequest request)
    {
        try
        {
            await _firebaseService.SendNotificationAsync(request.DeviceToken, request.Title, request.Body);
            _logger.LogInformation("Notification sent to device: {Token}", request.DeviceToken);
            return Ok(new { success = true, message = "Notification sent successfully!" });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to send notification");
            return BadRequest(new { success = false, message = ex.Message });
        }
    }

    /// <summary>
    /// G?i notification ??n m?t topic (t?t c? users ??ng ký topic ?ó)
    /// </summary>
    [HttpPost("send-to-topic")]
    public async Task<IActionResult> SendToTopic([FromBody] SendToTopicRequest request)
    {
        try
        {
            await _firebaseService.SendNotificationToTopicAsync(request.Topic, request.Title, request.Body);
            _logger.LogInformation("Notification sent to topic: {Topic}", request.Topic);
            return Ok(new { success = true, message = $"Notification sent to topic '{request.Topic}' successfully!" });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to send notification to topic");
            return BadRequest(new { success = false, message = ex.Message });
        }
    }

    /// <summary>
    /// Test endpoint - Ki?m tra Firebase ?ã ???c c?u hình ch?a
    /// </summary>
    [HttpGet("test")]
    public IActionResult TestFirebaseConnection()
    {
        try
        {
            var firebaseApp = FirebaseAdmin.FirebaseApp.DefaultInstance;
            if (firebaseApp != null)
            {
                return Ok(new 
                { 
                    success = true, 
                    message = "Firebase is configured correctly!",
                    projectId = firebaseApp.Options.ProjectId
                });
            }
            return Ok(new { success = false, message = "Firebase is not initialized" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { success = false, message = ex.Message });
        }
    }
}

public class SendNotificationRequest
{
    public string DeviceToken { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
}

public class SendToTopicRequest
{
    public string Topic { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
}
