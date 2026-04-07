using SchoolProject.Domain.Entities;

namespace SchoolProject.Domain.Interfaces
{
    public interface IEnrollmentRepository : IGenericRepositoryAsync<Enrollment>
    {
        Task<IQueryable<Enrollment>> GetAllEnrollmentsAsync();
        Task<Enrollment?> GetEnrollmentByIdAsync(int id);
        Task<bool> IsEnrolledAsync(int studentId, int courseId);
        Task<IQueryable<Enrollment>> GetEnrollmentsByStudentIdAsync(int studentId);
        Task<IQueryable<Enrollment>> GetEnrollmentsByCourseIdAsync(int courseId);

    }
}
