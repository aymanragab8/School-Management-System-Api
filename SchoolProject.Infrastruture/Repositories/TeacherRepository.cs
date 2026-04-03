using Microsoft.EntityFrameworkCore;
using SchoolProject.Domain.Entities;
using SchoolProject.Domain.Interfaces;
using SchoolProject.Infrastruture.Data;
using SchoolProject.Infrastruture.InfrastructureBases;

namespace SchoolProject.Infrastruture.Repositories
{
    public class TeacherRepository : GenericRepositoryAsync<Teacher>, ITeacherRepository
    {
        public TeacherRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IQueryable<Teacher>> GetAllTeachersAsync()
        {
            return await Task.FromResult(
                _dbContext.Teachers
                    .AsNoTracking()
            );
        }

        public async Task<Teacher> GetTeacherByIdAsync(int id)
        {
            return await _dbContext.Teachers
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<bool> IsNationalIdExistAsync(string nationalId)
        {
            return await _dbContext.Teachers
                .AnyAsync(s => s.NationalId == nationalId);
        }

    }
}
