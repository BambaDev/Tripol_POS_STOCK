using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pos.Models
{
    [Table("LoginAttempt")]
    public partial class LoginAttempt
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Username { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime AttemptTime { get; set; }

        public bool IsSuccessful { get; set; }

        [StringLength(45)]
        public string IpAddress { get; set; }

        [StringLength(500)]
        public string UserAgent { get; set; }

        [StringLength(500)]
        public string FailureReason { get; set; }
    }
}
