using Relmonitor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Relmonitor.IRepositories
{
    public interface IRelease
    {
        Task<IEnumerable<Release>> Get();
        Task<Release> GetByDen(string den);
        Task<Release> GetById(int id);
        Task<Release> Create(Release release);
        Task Update(Release release);
        Task Delete(int id);
    }
}
