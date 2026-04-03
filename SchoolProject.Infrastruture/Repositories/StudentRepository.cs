using Microsoft.EntityFrameworkCore;
using SchoolProject.Domain.Entities;
using SchoolProject.Domain.Interfaces;
using SchoolProject.Infrastruture.Data;
using SchoolProject.Infrastruture.InfrastructureBases;


namespace SchoolProject.Infrastruture.Repositories
{
    public class StudentRepository : GenericRepositoryAsync<Student>, IStudentRepository
    {
        private readonly ApplicationDbContext _context;

        public StudentRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IQueryable<Student>> GetAllStudentsAsync()
        {
            return await Task.FromResult(
                _context.Students
                    .Include(s => s.Grade)
                    .AsNoTracking()
            );
        }
        public async Task<IQueryable<Student>> GetStudentsByGradeAsync(int gradeId)
        {
            return await Task.FromResult(
                _context.Students
                    .Where(s => s.GradeId == gradeId)
                    .Include(s => s.Grade)
                    .AsNoTracking()
            );
        }
        public async Task<Student> GetStudentByIdAsync(int id)
        {
            return await _context.Students
                .Include(s => s.Grade)
                .Include(s => s.Enrollments)
                    .ThenInclude(e => e.Course)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<bool> IsNationalIdExistAsync(string nationalId)
        {
            return await _context.Students
                .AnyAsync(s => s.NationalId == nationalId);
        }
        public async Task<Student> GetStudentByUserIdAsync(string userId)
        {
            return await _context.Students
                .FirstOrDefaultAsync(s => s.ApplicationUserId == userId);
        }

    }
}