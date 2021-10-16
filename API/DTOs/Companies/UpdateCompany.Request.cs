using System.ComponentModel.DataAnnotations;

namespace API.DTOs.Compaines
{
    public class UpdateCompanyRequest
    {
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