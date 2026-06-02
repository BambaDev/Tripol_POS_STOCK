using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pos.Models
{
    [Table("UserSession")]
    public partial class UserSession
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(128)]
        public string Token { get; set; }

        [Required]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [Required]
        [Column(TypeName = "datetime")]
        public DateTime CreatedAt { get; set; }

        [Required]
        [Column(TypeName = "datetime")]
        public DateTime ExpiresAt { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? LastActivityAt { get; set; }

        [StringLength(45)]
        public string IpAddress { get; set; }

        [StringLength(500)]
        public string UserAgent { get; set; }

        public bool IsRevoked { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? RevokedAt { get; set; }
    }
}
