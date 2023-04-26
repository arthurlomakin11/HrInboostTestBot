using HrInboostTestBot.Data.Tracks;
using Microsoft.EntityFrameworkCore;

namespace HrInboostTestBot.Features;

public class TracksService
{
    private readonly TracksDbContext _context;
    
    public TracksService(TracksDbContext context)
    {
        _context = context;
    }

    public TrackLocationGroupTotals GetTracksTotals(string IMEI)
    {
        return _context.TrackLocationGroupTotals
            .FromSql($"EXEC getGroupedTracksTotals @IMEI = {IMEI}")
            .ToList()
            .First();
    }
    
    public List<TrackLocationGroup> GetTop10Tracks(string IMEI)
    {
        return _context.TopTracks
            .FromSql($"EXEC getTop10Tracks @IMEI = {IMEI}")
            .ToList();
    }
}