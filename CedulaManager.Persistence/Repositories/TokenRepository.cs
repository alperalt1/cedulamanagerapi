using CedulaManager.Domain.Entities;
using CedulaManager.Domain.Interfaces;
using CedulaManager.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CedulaManager.Persistence.Repositories
{
    public class TokenRepository: ITokenRepository
    {
        private readonly ApplicationDbContext _context;

        public TokenRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddTokenAsync(RefreshToken refreshToken)
        {
            await _context.RefreshTokens.AddAsync(refreshToken);
        }

        public async Task SaveChangesTokenAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
