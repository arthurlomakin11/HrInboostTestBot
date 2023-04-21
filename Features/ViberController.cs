﻿using Microsoft.AspNetCore.Mvc;
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

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] ViberCallbackData update)
    {
        var str = string.Empty;

        if (update.Event is ViberEventType.Webhook or ViberEventType.Seen or ViberEventType.Delivered)
        {
            return Ok();
        }
        
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