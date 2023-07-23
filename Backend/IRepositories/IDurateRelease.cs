using Relmonitor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Relmonitor.IRepositories
{
    public interface IDurateRelease
    {
        Task<IEnumerable<Duraterelease>> Get();
        Task<Duraterelease> GetByDen(string den);
        Task<Duraterelease> GetById(int id);
        Task<Duraterelease> Create(Duraterelease durate);
        Task Update(Duraterelease durate);
        Task Delete(int id);
    }
}
