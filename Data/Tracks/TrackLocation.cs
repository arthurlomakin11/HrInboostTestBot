using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HrInboostTestBot.Data.Tracks;

public class TrackLocation
{
    [Key]
    [Column("id")]
    public int Id { get; set; }
    
    [MaxLength(50)]
    public string IMEI { get; set; }
    
    [Column(TypeName = "decimal(12,9)")]
    public decimal Latitude { get; set; }
    
    [Column(TypeName = "decimal(12,9)")]
    public decimal Longitude { get; set; }
    
    public DateTime DateTrack { get; set; }
}