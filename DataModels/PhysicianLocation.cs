﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HalloDoc.DataModels;

[Table("PhysicianLocation")]
public partial class PhysicianLocation
{
    [Key]
    [Column("LocationId ")]
    public int LocationId { get; set; }

    public int PhysicianId { get; set; }

    [Column("Latitude ")]
    [Precision(10, 0)]
    public decimal? Latitude { get; set; }

    [Precision(10, 0)]
    public decimal? Longitude { get; set; }

    public DateTime? CreatedDate { get; set; }

    [StringLength(56)]
    public string? PhysicianName { get; set; }

    [StringLength(556)]
    public string? Address { get; set; }

    [ForeignKey("PhysicianId")]
    [InverseProperty("PhysicianLocations")]
    public virtual Physician Physician { get; set; } = null!;
}