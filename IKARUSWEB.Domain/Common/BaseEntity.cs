namespace IKARUSWEB.Domain.Common
{
    public abstract class BaseEntity
    {
        public Guid Id { get; protected set; }
        public bool IsActive { get; protected set; } = true;
        public bool IsDeleted { get; protected set; } = false;

        public string CreatedUser { get; protected set; } = default!;
        public DateTime CreatedDate { get; protected set; }

        public string? ModifiedUser { get; protected set; }
        public DateTime? ModifiedDate { get; protected set; }

        public DateTime? DeletedDate { get; protected set; }

        protected BaseEntity() { }

        protected BaseEntity(string createdUser)
        {
            Id = Guid.NewGuid();
            CreatedUser = createdUser;
            CreatedDate = DateTime.UtcNow;
        }

        public void MarkModified(string modifiedUser)
        {
            ModifiedUser = modifiedUser;
            ModifiedDate = DateTime.UtcNow;
        }

        public void MarkDeleted()
        {
            IsDeleted = true;
            IsActive = false;
            DeletedDate = DateTime.UtcNow;
        }
    }
}
