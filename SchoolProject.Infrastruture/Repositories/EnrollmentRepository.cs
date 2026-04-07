using Microsoft.EntityFrameworkCore;
using SchoolProject.Domain.Entities;
using SchoolProject.Domain.Enums;
using SchoolProject.Domain.Interfaces;
using SchoolProject.Infrastruture.Data;
using SchoolProject.Infrastruture.InfrastructureBases;


namespace SchoolProject.Infrastruture.Repositories
{
    public class EnrollmentRepository : GenericRepositoryAsync<Enrollment>, IEnrollmentRepository
    {
        public EnrollmentRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IQueryable<Enrollment>> GetAllEnrollmentsAsync()
        {
            return await Task.FromResult(
                _dbContext.Enrollments
                     .AsNoTracking());
        }

        public async Task<Enrollment> GetEnrollmentByIdAsync(int id)
        {
            return await _dbContext.Enrollments
                 .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<IQueryable<Enrollment>> GetEnrollmentsByCourseIdAsync(int courseId)
        {
            return await Task.FromResult(
                _dbContext.Enrollments
                    .Include(e => e.Course)
                    .Include(e => e.Student)
                    .Where(e => e.CourseId == courseId)
                    .AsNoTracking()
            );
        }

        public async Task<IQueryable<Enrollment>> GetEnrollmentsByStudentIdAsync(int studentId)
        {
            return await Task.FromResult(
                _dbContext.Enrollments
                    .Include(e => e.Student)
                    .Where(e => e.StudentId == studentId)
                    .AsNoTracking()
            );
        }

        public async Task<bool> IsEnrolledAsync(int studentId, int courseId)
        {
            return await _dbContext.Enrollments
                   .AnyAsync(e => e.StudentId == studentId
                && e.CourseId == courseId
                && e.Status != EnrollmentStatus.Dropped);
        }
    }
}
