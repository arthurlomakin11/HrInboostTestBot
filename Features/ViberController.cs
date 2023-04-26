using HrInboostTestBot.Data.Tracks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Viber.Bot.NetCore.Infrastructure;
using Viber.Bot.NetCore.Models;
using Viber.Bot.NetCore.RestApi;

namespace HrInboostTestBot.Features;

[Route("/viber")]
[ApiController]
public class ViberController : ControllerBase
{
    private readonly TracksDbContext _context;
    private readonly IViberBotApi _viberBotApi;

    public ViberController(IViberBotApi viberBotApi, TracksDbContext context)
    {
        _viberBotApi = viberBotApi;
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] ViberCallbackData update)
    {
        Console.WriteLine(JsonConvert.SerializeObject(update));

        if (update.Event == ViberEventType.Message && update.Message.Type == ViberMessageType.Text 
                || update.Event == ViberEventType.ConversationStarted)
        {
            var _handler = new ViberMessageHandler(_viberBotApi, _context, update);
            
            var mess = update.Message as ViberMessage.TextMessage;
        
            var str = mess?.Text;

            if(!_handler.IsUserRecorded())
            {
                if (_handler.IsInputValueEmpty())
                {
                    await _handler.SendGreetings();
                }
                else
                {
                    _handler.SetInputValueAsIMEI();
                    await _handler.SendTotals();
                }
            }
            else if (str == "Top10Walks") await _handler.SendTop10Tracks();
            else if (str == "TotalSummary") await _handler.SendTotals();
        }

        return Ok();
    }
}