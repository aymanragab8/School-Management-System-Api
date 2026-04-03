using SchoolProject.Domain.Enums;

namespace SchoolProject.Domain.Entities
{
    public class Person : BaseEntity
    {
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string NationalId { get; set; }
        public string Address { get; set; }
        public Gender Gender { get; set; }
        public string? ApplicationUserId { get; set; }

    }
}
