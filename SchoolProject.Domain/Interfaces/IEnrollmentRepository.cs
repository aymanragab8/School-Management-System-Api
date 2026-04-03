using SchoolProject.Domain.Entities;

namespace SchoolProject.Domain.Interfaces
{
    public interface IEnrollmentRepository : IGenericRepositoryAsync<Enrollment>
    {
        Task<IQueryable<Enrollment>> GetAllEnrollmentsAsync();
        Task<Enrollment?> GetEnrollmentByIdAsync(int id);
        Task<bool> IsEnrolledAsync(int studentId, int courseId);
        Task<List<Enrollment>> GetEnrollmentsByStudentIdAsync(int studentId);
        Task<List<Enrollment>> GetEnrollmentsByCourseIdAsync(int courseId);

    }
}
