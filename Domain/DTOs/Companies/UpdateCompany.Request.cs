using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.DTOs.Companies
{
    public class UpdateCompanyRequest : CompanyBaseRequest
    {
        [JsonIgnore]
        public new int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(255)]
        public string Address { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

    }
}