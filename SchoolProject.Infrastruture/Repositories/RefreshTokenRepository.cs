namespace SchoolProject.Infrastruture.Repositories
{
    using global::SchoolProject.Domain.Entities;
    using global::SchoolProject.Domain.Interfaces;
    using global::SchoolProject.Infrastruture.Data;
    using global::SchoolProject.Infrastruture.InfrastructureBases;
    using Microsoft.EntityFrameworkCore;

    namespace SchoolProject.Infrastructure.Repositories
    {
        public class RefreshTokenRepository : GenericRepositoryAsync<RefreshToken>, IRefreshTokenRepository
        {
            private readonly ApplicationDbContext _context;

            public RefreshTokenRepository(ApplicationDbContext context) : base(context)
            {
                _context = context;
            }

            public async Task<RefreshToken?> GetByTokenAsync(string token)
            {
                return await _context.RefreshTokens
                    .FirstOrDefaultAsync(r => r.Token == token && !r.IsRevoked);
            }
        }
    }
}
