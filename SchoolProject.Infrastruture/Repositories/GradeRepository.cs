using Microsoft.EntityFrameworkCore;
using SchoolProject.Domain.Entities;
using SchoolProject.Domain.Interfaces;
using SchoolProject.Infrastruture.Data;
using SchoolProject.Infrastruture.InfrastructureBases;

namespace SchoolProject.Infrastruture.Repositories
{
    public class GradeRepository : GenericRepositoryAsync<Grade>, IGradeRepository
    {
        public GradeRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IQueryable<Grade>> GetAllGradesAsync()
        {
            return await Task.FromResult(
                _dbContext.Grades
                    .AsNoTracking());
        }

        public async Task<Grade> GetGradeByIdAsync(int id)
        {
            return await _dbContext.Grades
                    .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<bool> IsLevelExistAsync(int levellId)
        {
            return await _dbContext.Grades
                .AnyAsync(s => s.Level == levellId);
        }

    }
}
