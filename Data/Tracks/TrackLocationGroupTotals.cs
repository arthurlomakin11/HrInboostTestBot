using Microsoft.EntityFrameworkCore;

namespace HrInboostTestBot.Data.Tracks;

[Keyless]
public class TrackLocationGroupTotals
{
    public int WalksCount { get; set; }

    public int TotalDurationMinutes { get; set; }
    
    public double TotalDistanceMeters { get; set; }
}