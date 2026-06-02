using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Pos.Models;

[Table("UserHasRole")]
[Index("RoleId", Name = "IX_UserHasRole_RoleId")]
[Index("UserId", Name = "IX_UserHasRole_UserId")]
public partial class UserHasRole
{
    [Key]
    public int Id { get; set; }

    public int RoleId { get; set; }

    public int UserId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }

    [ForeignKey("RoleId")]
    [InverseProperty("UserHasRoles")]
    public virtual Role Role { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("UserHasRoles")]
    public virtual User User { get; set; }
}
