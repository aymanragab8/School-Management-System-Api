using SchoolProject.Domain.Entities;

namespace SchoolProject.Domain.Interfaces
{
    public interface ITeacherRepository : IGenericRepositoryAsync<Teacher>
    {
        Task<IQueryable<Teacher>> GetAllTeachersAsync();
        Task<Teacher> GetTeacherByIdAsync(int id);
        Task<bool> IsNationalIdExistAsync(string nationalId);
    }
}
