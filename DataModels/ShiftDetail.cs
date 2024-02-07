using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HalloDoc.DataModels;

[Table("ShiftDetail")]
public partial class ShiftDetail
{
    [Key]
    public int ShiftDetailId { get; set; }

    public int ShiftId { get; set; }

    public DateTime ShiftDate { get; set; }

    [Column("RegionId ")]
    public int? RegionId { get; set; }

    [Column(TypeName = "time with time zone")]
    public DateTimeOffset StartTime { get; set; }

    [Column(TypeName = "time with time zone")]
    public DateTimeOffset EndTime { get; set; }

    public short Status { get; set; }

    [Column(TypeName = "bit(1)")]
    public BitArray IsDeleted { get; set; } = null!;

    [StringLength(128)]
    public string? ModifiedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public DateTime? LastRunningDate { get; set; }

    [Column("EventId ")]
    [StringLength(100)]
    public string? EventId { get; set; }

    [Column(TypeName = "bit(1)")]
    public BitArray? IsSync { get; set; }

    [ForeignKey("ModifiedBy")]
    [InverseProperty("ShiftDetails")]
    public virtual AspNetUser? ModifiedByNavigation { get; set; }

    [ForeignKey("RegionId")]
    [InverseProperty("ShiftDetails")]
    public virtual Region? Region { get; set; }

    [ForeignKey("ShiftId")]
    [InverseProperty("ShiftDetails")]
    public virtual Shift Shift { get; set; } = null!;

    [InverseProperty("ShiftDetail")]
    public virtual ICollection<ShiftDetailRegion> ShiftDetailRegions { get; } = new List<ShiftDetailRegion>();
}
