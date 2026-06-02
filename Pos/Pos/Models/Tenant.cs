using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Pos.Models;

[Table("Tenant")]
public partial class Tenant
{
    [Key]
    public Guid Id { get; set; }

    [StringLength(250)]
    public string Name { get; set; }

    [StringLength(250)]
    public string DomainName { get; set; }

    [StringLength(250)]
    public string ConnectionString { get; set; }

    [StringLength(250)]
    public string IsActive { get; set; }

    [StringLength(250)]
    public string SubscriptionType { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? SubscriptionExpiry { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }
}
