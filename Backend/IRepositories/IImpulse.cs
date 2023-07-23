using Relmonitor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Relmonitor.IRepositories
{
    public interface IImpulse
    {
        Task<IEnumerable<Impulse>> Get();
        Task<Impulse> GetByDen(string den);
        Task<Impulse> GetById(int id);
        Task<Impulse> Create(Impulse impulse);
        Task Update(Impulse impulse);
        Task Delete(int id);
    }
}
