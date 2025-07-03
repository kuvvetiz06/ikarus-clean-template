using IKARUSWEB.Domain.Common;

namespace IKARUSWEB.Domain.Entities
{
    public class Tenant : BaseEntity
    {
        public string Name { get; private set; } = default!;
        public string Code { get; private set; } = default!;
        public string Address { get; private set; } = default!;
        public string PhoneNumber { get; private set; } = default!;
        public string Email { get; private set; } = default!;

        private Tenant() { }

        // Güncellenmiş ctor: name ve createdUser yanı sıra Code vs alıyor
        public Tenant(string code, string name, string address, string phoneNumber, string email, string createdUser)
            : base(createdUser)
        {
            SetCode(code);
            SetName(name);
            SetAddress(address);
            SetPhoneNumber(phoneNumber);
            SetEmail(email);
        }

        public void SetCode(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
                throw new ArgumentException("Code boş olamaz.", nameof(code));
            Code = code.Trim();
            MarkModified(ModifiedUser ?? CreatedUser);
        }

        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Tenant adı boş olamaz.", nameof(name));
            Name = name.Trim();
            MarkModified(ModifiedUser ?? CreatedUser);
        }

        public void SetAddress(string address)
        {
            if (string.IsNullOrWhiteSpace(address))
                throw new ArgumentException("Adres boş olamaz.", nameof(address));
            Address = address.Trim();
            MarkModified(ModifiedUser ?? CreatedUser);
        }

        public void SetPhoneNumber(string phone)
        {
            // Basit örnek validasyon
            if (string.IsNullOrWhiteSpace(phone))
                throw new ArgumentException("Telefon numarası boş olamaz.", nameof(phone));
            PhoneNumber = phone.Trim();
            MarkModified(ModifiedUser ?? CreatedUser);
        }

        public void SetEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
                throw new ArgumentException("Geçerli bir e-posta girin.", nameof(email));
            Email = email.Trim();
            MarkModified(ModifiedUser ?? CreatedUser);
        }

        public void Deactivate(string modifiedUser)
        {
            IsActive = false;
            MarkModified(modifiedUser);
        }
    }
}

