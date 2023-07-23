using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Relmonitor.Models;

namespace Relmonitor.IRepositories
{
    public interface IDetaliiRelease
    {
        Task<IEnumerable<Detaliirelease>> Get();
        Task<IEnumerable<Detaliirelease>> GetByDen(string den);
        Task<Detaliirelease> GetById(int id);
        Task<Detaliirelease> Create(Detaliirelease detalii);
        Task Update(Detaliirelease detalii);
        Task Delete(int id);
        Task<IEnumerable<Detaliirelease>> GetMatchDate(DateTime date);
    }
}
