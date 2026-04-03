using SchoolProject.Domain.Entities;

namespace SchoolProject.Domain.Interfaces
{
    public interface IGradeRepository : IGenericRepositoryAsync<Grade>
    {
        Task<IQueryable<Grade>> GetAllGradesAsync();
        Task<Grade> GetGradeByIdAsync(int id);
        Task<bool> IsLevelExistAsync(int levellId);
    }
}
