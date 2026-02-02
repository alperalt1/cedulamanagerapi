using CedulaManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CedulaManager.Domain.Interfaces
{
    public interface ITokenRepository
    {
        Task AddTokenAsync(RefreshToken usuario);
        Task SaveChangesTokenAsync();
    }
}
