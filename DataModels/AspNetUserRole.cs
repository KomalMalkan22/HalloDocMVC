﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HalloDoc.DataModels;

[PrimaryKey("UserId", "RoleId")]
public partial class AspNetUserRole
{
    [Key]
    [StringLength(128)]
    public string UserId { get; set; } = null!;

    [Key]
    [Column("RoleId ")]
    [StringLength(128)]
    public string RoleId { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("AspNetUserRoles")]
    public virtual Aspnetuser User { get; set; } = null!;
}
