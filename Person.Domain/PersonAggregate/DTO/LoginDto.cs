using System.ComponentModel.DataAnnotations;

namespace Person.Domain.PersonAggregate.DTO
{
    public class LoginDto
    {
        [Required]
        public int LoginId { get; set; }
        [MaxLength(36)]
        public string? Uuid { get; set; }
        [MaxLength(25)]
        public string? Username { get; set; }
        [MaxLength(25)]
        public string? Password { get; set; }
        [MaxLength(10)]
        public string? Salt { get; set; }
        [MaxLength(32)]
        public string? Md5 { get; set; }
        [MaxLength(40)]
        public string? Sha1 { get; set; }
        [MaxLength(64)]
        public string? Sha256 { get; set; }
    }
}