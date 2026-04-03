using SchoolProject.Domain.Entities;

namespace SchoolProject.Domain.Interfaces
{
    public interface IRefreshTokenRepository : IGenericRepositoryAsync<RefreshToken>
    {
        Task<RefreshToken?> GetByTokenAsync(string token);
    }
}
