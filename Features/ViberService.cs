using Viber.Bot.NetCore.Models;
using Viber.Bot.NetCore.RestApi;

namespace HrInboostTestBot.Features;

public class ViberService
{
    private readonly IViberBotApi _viberBotApi;
    public ViberService(IViberBotApi viberBotApi)
    {
        _viberBotApi = viberBotApi;
    }

    public async Task SendTextMessage(string receiver, string text)
    {
        var message = new ViberMessage.TextMessage
        {
            Receiver = receiver,
            Text = text
        };
            
        await _viberBotApi.SendMessageAsync<ViberResponse.SendMessageResponse>(message);
    }
    
    public async Task SendTextMessageWithButton(string receiver, string text, string buttonText, string buttonActionBody)
    {
        var message = new ViberMessage.KeyboardMessage
        {
            Receiver = receiver,
            Keyboard = new ViberKeyboard
            {
                Buttons = new List<ViberKeyboardButton>
                {
                    new()
                    {
                        Text = buttonText,
                        ActionBody = buttonActionBody
                    }
                }
            },
            Text = text
        };
                
        await _viberBotApi.SendMessageAsync<ViberResponse.SendMessageResponse>(message);
    }
}