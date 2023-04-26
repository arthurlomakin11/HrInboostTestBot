using HrInboostTestBot.Data.Tracks;
using HrInboostTestBot.Data.ViberUsersHash;
using Viber.Bot.NetCore.Models;
using Viber.Bot.NetCore.RestApi;

namespace HrInboostTestBot.Features;

public class ViberMessageHandler
{
    private readonly ViberService _viberService;
    private readonly TracksService _tracksService;
    private readonly string _userId;
    private readonly string? _str;
    
    public ViberMessageHandler(IViberBotApi viberBotApi, TracksDbContext context, ViberCallbackData update)
    {
        _viberService = new ViberService(viberBotApi);
        _tracksService = new TracksService(context);

        _userId = update.Sender != null ? update.Sender.Id! : update.User.Id!;

        _str = (update.Message as ViberMessage.TextMessage)?.Text;
    }

    public async Task SendTotals()
    {
        var trackLocationGroups = _tracksService.GetTracksTotals(UsersHashTable.GetValue(_userId));

        var strMessage = $"Всего прогулок: {trackLocationGroups.WalksCount}\n" +
                         $"Всего км пройдено: {double.Floor(trackLocationGroups.TotalDistanceMeters / 1000)}\n" +
                         $"Всего времени,мин: {double.Floor(trackLocationGroups.TotalDurationMinutes)}";
                    
        await _viberService.SendTextMessageWithButton(_userId, 
            strMessage, 
            "ТОП 10 прогулок", 
            "Top10Walks");
    }
    
    public async Task SendTop10Tracks()
    {
        var topTracks = _tracksService.GetTop10Tracks(UsersHashTable.GetValue(_userId)).ToList();

        var text = "ТОП 10 прогулок по длительности:\n";
        foreach (var track in topTracks)
        {
            text += $"Длительность: {double.Floor(track.DurationMinutes)} | Расстояние: {double.Floor(track.DistanceMeters)}\n";
        }
        
        await _viberService.SendTextMessageWithButton(_userId, 
            text, 
            "Назад", 
            "TotalSummary");
    }
    
    public async Task SendGreetings()
    {
        await _viberService.SendTextMessage(_userId, "Привет, введи свой IMEI");
    }
    
    public bool IsUserRecorded()
    {
        return UsersHashTable.ContainsKey(_userId);
    }
    
    public void SetInputValueAsIMEI()
    {
        UsersHashTable.Add(_userId, _str);
    }
    
    public bool IsInputValueEmpty()
    {
        return string.IsNullOrEmpty(_str);
    }
}