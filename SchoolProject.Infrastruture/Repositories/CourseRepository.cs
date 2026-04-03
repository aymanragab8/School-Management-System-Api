using Microsoft.EntityFrameworkCore;
using SchoolProject.Domain.Entities;
using SchoolProject.Domain.Interfaces;
using SchoolProject.Infrastruture.Data;
using SchoolProject.Infrastruture.InfrastructureBases;

namespace SchoolProject.Infrastruture.Repositories
{
    public class CourseRepository : GenericRepositoryAsync<Course>, ICourseRepository
    {
        public CourseRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IQueryable<Course>> GetAllCoursesAsync()
        {
            return await Task.FromResult(
             _dbContext.Courses
                 .AsNoTracking());
        }

        public async Task<Course> GetCourseByIdAsync(int id)
        {
            return await _dbContext.Courses
                 .FirstOrDefaultAsync(s => s.Id == id);
        }


        public async Task<IQueryable<Course>> GetCoursesByTeacherAsync(int teacherId)
        {
            return await Task.FromResult(
            _dbContext.Courses
                .Where(s => s.TeacherId == teacherId)
                .Include(s => s.Teacher)
                .AsNoTracking());
        }
        public async Task<IQueryable<Course>> GetCoursesByGradeIdAsync(int gradeId)
        {
            return await Task.FromResult(
                _dbContext.Courses
                    .Where(s => s.GradeId == gradeId)
                    .Include(s => s.Grade)
                    .AsNoTracking()
            );
        }
    }
}
