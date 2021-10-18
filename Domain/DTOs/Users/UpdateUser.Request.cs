using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.DTOs.Users
{
    public class UpdateUserRequest : UserBaseRequest
    {

        [JsonIgnore]
        public new int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Address { get; set; }
        [Required]
        [StringLength(125)]
        public string Password { get; set; }

        [StringLength(255)]
        public string Email { get; set; }

        [Required]
        public int CompanyId { get; set; }
    }
}