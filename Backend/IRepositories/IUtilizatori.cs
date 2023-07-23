using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Relmonitor.Models;

namespace Relmonitor.IRepositories
{
    public interface IUtilizatori
    {
        Task<IEnumerable<Utilizatori>> Get();
        Task<Utilizatori> GetByDen(string den);
        Task<Utilizatori> GetById(int id);
        Task<Utilizatori> Create(Utilizatori utiilizatori);
        Task Update(Utilizatori utiilizatori);
        Task Delete(int id);
    }
}
