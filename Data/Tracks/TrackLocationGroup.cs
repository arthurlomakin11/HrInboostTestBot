using Microsoft.EntityFrameworkCore;

namespace HrInboostTestBot.Data.Tracks;

[Keyless]
public class TrackLocationGroup
{
    public int WalkNum { get; set; }

    public DateTime StartTime { get; set; }
    
    public DateTime EndTime { get; set; }
    
    public int DurationMinutes { get; set; }
    
    public double DistanceMeters { get; set; }
}