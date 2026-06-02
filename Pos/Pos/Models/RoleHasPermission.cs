using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Pos.Models;

[Table("RoleHasPermission")]
[Index("PermissionId", Name = "IX_RoleHasPermission_PermissionId")]
[Index("RoleId", Name = "IX_RoleHasPermission_RoleId")]
public partial class RoleHasPermission
{
    [Key]
    public int Id { get; set; }

    public int PermissionId { get; set; }

    public int RoleId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [ForeignKey("PermissionId")]
    [InverseProperty("RoleHasPermissions")]
    public virtual Permission Permission { get; set; }

    [ForeignKey("RoleId")]
    [InverseProperty("RoleHasPermissions")]
    public virtual Role Role { get; set; }
}
