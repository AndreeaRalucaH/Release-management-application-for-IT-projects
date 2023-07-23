using Relmonitor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Relmonitor.IRepositories
{
    public interface IAplicatii
    {
        Task<IEnumerable<Aplicatii>> Get();
        Task<Aplicatii> GetByDen(string den);
        Task<Aplicatii> GetById(int id);
        Task<Aplicatii> Create(Aplicatii app);
        Task Update(Aplicatii com);
        Task Delete(int id);
    }
}
