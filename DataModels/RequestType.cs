﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HalloDoc.DataModels;

[Table("RequestType")]
public partial class RequestType
{
    [Key]
    public int RequestTypeId { get; set; }

    [Column("Name ")]
    [StringLength(56)]
    public string Name { get; set; } = null!;

    [InverseProperty("RequestType")]
    public virtual ICollection<Request> Requests { get; } = new List<Request>();
}