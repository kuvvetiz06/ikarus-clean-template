using IKARUSWEB.Domain.Common;

namespace IKARUSWEB.Domain.Entities
{
    public class Tenant : BaseEntity
    {
        public string Name { get; private set; } = default!;

        private Tenant() { }

        public Tenant(string name, string createdUser)
            : base(createdUser)
        {
            SetName(name);
        }

        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Tenant adı boş olamaz.", nameof(name));
            Name = name;
            MarkModified(ModifiedUser ?? CreatedUser);
        }

        public void Deactivate(string modifiedUser)
        {
            IsActive = false;
            MarkModified(modifiedUser);
        }
    }
}

