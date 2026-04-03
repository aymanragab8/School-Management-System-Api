using SchoolProject.Domain.Entities;

namespace SchoolProject.Domain.Interfaces
{
    public interface IStudentRepository : IGenericRepositoryAsync<Student>
    {
        Task<IQueryable<Student>> GetAllStudentsAsync();
        Task<Student> GetStudentByIdAsync(int id);
        Task<IQueryable<Student>> GetStudentsByGradeAsync(int gradeId);
        Task<bool> IsNationalIdExistAsync(string nationalId);
        Task<Student> GetStudentByUserIdAsync(string userId);
    }
}
