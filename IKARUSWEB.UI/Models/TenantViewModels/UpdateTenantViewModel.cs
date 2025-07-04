using System.ComponentModel.DataAnnotations;

namespace IKARUSWEB.UI.Models.TenantViewModels
{
    public class UpdateTenantViewModel
    {
        [Required]
        public Guid Id { get; set; }

        [Required, StringLength(50)]
        public string Code { get; set; } = default!;

        [Required, StringLength(200)]
        public string Name { get; set; } = default!;

        [Required]
        public string Address { get; set; } = default!;

        [Required]
        public string PhoneNumber { get; set; } = default!;

        [Required, EmailAddress]
        public string Email { get; set; } = default!;

        [Required]
        public string ModifiedUser { get; set; } = default!;

    }
}
