using SchoolProject.Domain.Entities;

namespace SchoolProject.Domain.Interfaces
{
    public interface ICourseRepository : IGenericRepositoryAsync<Course>
    {
        Task<IQueryable<Course>> GetAllCoursesAsync();
        Task<Course> GetCourseByIdAsync(int id);
        Task<IQueryable<Course>> GetCoursesByTeacherAsync(int teacherId);
        Task<IQueryable<Course>> GetCoursesByGradeIdAsync(int gradeId);
    }
}
