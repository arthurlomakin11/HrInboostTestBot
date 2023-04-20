using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Viber.Bot.NetCore.Infrastructure;
using Viber.Bot.NetCore.Models;
using Viber.Bot.NetCore.RestApi;

namespace HrInboostTestBot.Features;

[Route("/viber")]
[ApiController]
public class ViberController : ControllerBase
{
    private readonly IViberBotApi _viberBotApi;
    
    public ViberController(IViberBotApi viberBotApi)
    {
        _viberBotApi = viberBotApi;
    }

    // The service sets a webhook automatically, but if you want sets him manually then use this
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var response = await _viberBotApi.SetWebHookAsync(new ViberWebHook.WebHookRequest("https://1981-109-207-199-125.ngrok-free.app/viber"));

        if (response.Content.Status == ViberErrorCode.Ok)
        {
            return Ok("Viber-bot is active");
        }

        return BadRequest(response.Content.StatusMessage);
    }

    [HttpPost]
    public async Task<IActionResult> Post()
    {
        var str = string.Empty;

        using var reader = new StreamReader(Request.Body);
        var body = await reader.ReadToEndAsync();

        if (body.Length == 0) return BadRequest();
        
        var update = JsonSerializer.Deserialize<ViberCallbackData>(body)!;

        if (update.Event == ViberEventType.Webhook) return Ok();
        
        Console.WriteLine(update.Event);
        if (update.Message.Type == ViberMessageType.Text)
        {
            var mess = update.Message as ViberMessage.TextMessage;

            str = mess.Text;
        }

        // you should to control required fields
        var message = new ViberMessage.TextMessage
        {
            Receiver = update.Sender.Id,
            Sender = new ViberUser.User
            {
                Name = "TestBotWalks",
                Avatar = "https://dl-media.viber.com/1/share/2/long/vibes/icon/image/0x0/f6a0/6bd2e2100a5d467c38d9776b8bb7aa500996eea5500eb6a6ffb6127078f4f6a0.jpg"
            },
            //required
            Text = str
        };

        // our bot returns incoming text
        var response = await _viberBotApi.SendMessageAsync<ViberResponse.SendMessageResponse>(message);

        return Ok();
    }
}