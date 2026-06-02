using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Pos.Models;

[Table("Tracking")]
public partial class Tracking
{
    [Key]
    public int Id { get; set; }

    public int UserId { get; set; }

    public int DeliveryId { get; set; }

    public int? CountryId { get; set; }

    public int? StateId { get; set; }

    public int? CityId { get; set; }

    public int? DeliveredBy { get; set; }

    public int? RouteId { get; set; }

    [StringLength(250)]
    public string Code { get; set; }

    [Column(TypeName = "decimal(9, 6)")]
    public decimal? Latitude { get; set; }

    [Column(TypeName = "decimal(9, 6)")]
    public decimal? Longitude { get; set; }

    public bool IsFinalUpdate { get; set; }

    [StringLength(250)]
    public string LocationDetails { get; set; }

    [Column(TypeName = "decimal(9, 2)")]
    public decimal? DistanceTravelled { get; set; }

    public bool? SignatureCaptured { get; set; }

    [StringLength(250)]
    public string WeatherConditions { get; set; }

    [StringLength(250)]
    public string VehicleSpeed { get; set; }

    [StringLength(250)]
    public string Remarks { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdateAt { get; set; }

    [ForeignKey("CityId")]
    [InverseProperty("Trackings")]
    public virtual City City { get; set; }

    [ForeignKey("CountryId")]
    [InverseProperty("Trackings")]
    public virtual Country Country { get; set; }

    [ForeignKey("DeliveredBy")]
    [InverseProperty("Trackings")]
    public virtual Employee DeliveredByNavigation { get; set; }

    [ForeignKey("StateId")]
    [InverseProperty("Trackings")]
    public virtual State State { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("Trackings")]
    public virtual User User { get; set; }
}
