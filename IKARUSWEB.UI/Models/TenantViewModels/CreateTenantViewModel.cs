using System;
using System.ComponentModel.DataAnnotations;

namespace IKARUSWEB.UI.Models.TenantViewModels
{
    public class CreateTenantViewModel
    {
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
        public string CreatedUser { get; set; } = default!;
    }
}
