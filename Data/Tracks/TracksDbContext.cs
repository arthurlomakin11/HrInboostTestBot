using Microsoft.EntityFrameworkCore;

namespace HrInboostTestBot.Data.Tracks;

public class TracksDbContext: DbContext
{
    public TracksDbContext(DbContextOptions<TracksDbContext> options) : base(options) { }
    
    public DbSet<TrackLocation> TrackLocations { get; set; }
    public DbSet<TrackLocationGroupTotals> TrackLocationGroupTotals { get; set; }
    public DbSet<TrackLocationGroup> TopTracks { get; set; }
}