using Relmonitor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Relmonitor.IRepositories
{
    public interface IMedii
    {
        Task<IEnumerable<Medii>> Get();
        Task<Medii> GetByDen(string den);
        Task<Medii> GetById(int id);
        Task<Medii> Create(Medii medii);
        Task Update(Medii medii);
        Task Delete(int id);
    }
}
