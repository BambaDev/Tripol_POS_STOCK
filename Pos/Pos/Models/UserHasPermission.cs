using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Pos.Models;

[Table("UserHasPermission")]
[Index("PermissionId", Name = "IX_UserHasPermission_PermissionId")]
[Index("UserId", Name = "IX_UserHasPermission_UserId")]
public partial class UserHasPermission
{
    [Key]
    public int Id { get; set; }

    public int PermissionId { get; set; }

    public int UserId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [ForeignKey("PermissionId")]
    [InverseProperty("UserHasPermissions")]
    public virtual Permission Permission { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("UserHasPermissions")]
    public virtual User User { get; set; }
}
