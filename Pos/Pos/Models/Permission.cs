using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Pos.Models;

[Table("Permission")]
public partial class Permission
{
    [Key]
    public int Id { get; set; }

    [StringLength(250)]
    public string Name { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [InverseProperty("Permission")]
    public virtual ICollection<RoleHasPermission> RoleHasPermissions { get; set; } = new List<RoleHasPermission>();

    [InverseProperty("Permission")]
    public virtual ICollection<UserHasPermission> UserHasPermissions { get; set; } = new List<UserHasPermission>();
}
