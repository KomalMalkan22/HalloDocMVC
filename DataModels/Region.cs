using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HalloDoc.DataModels;

[Table("Region")]
public partial class Region
{
    [Key]
    public int RegionId { get; set; }

    [StringLength(56)]
    public string Name { get; set; } = null!;

    [StringLength(56)]
    public string? Abbreviation { get; set; }

    [InverseProperty("Region")]
    public virtual ICollection<AdminRegion> AdminRegions { get; } = new List<AdminRegion>();

    [InverseProperty("Region")]
    public virtual ICollection<Admin> Admins { get; } = new List<Admin>();

    [InverseProperty("Region")]
    public virtual ICollection<Business> Businesses { get; } = new List<Business>();

    [InverseProperty("Region")]
    public virtual ICollection<Concierge> Concierges { get; } = new List<Concierge>();

    [InverseProperty("Region")]
    public virtual ICollection<HealthProfessional> HealthProfessionals { get; } = new List<HealthProfessional>();

    [InverseProperty("Region")]
    public virtual ICollection<PhysicianRegion> PhysicianRegions { get; } = new List<PhysicianRegion>();

    [InverseProperty("Region")]
    public virtual ICollection<Physician> Physicians { get; } = new List<Physician>();

    [InverseProperty("Region")]
    public virtual ICollection<RequestClient> RequestClients { get; } = new List<RequestClient>();

    [InverseProperty("Region")]
    public virtual ICollection<ShiftDetailRegion> ShiftDetailRegions { get; } = new List<ShiftDetailRegion>();

    [InverseProperty("Region")]
    public virtual ICollection<ShiftDetail> ShiftDetails { get; } = new List<ShiftDetail>();

    [InverseProperty("Region")]
    public virtual ICollection<User> Users { get; } = new List<User>();
}
