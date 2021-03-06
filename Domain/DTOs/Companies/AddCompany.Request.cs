using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs.Compaines
{
    public class AddCompanyRequest
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