using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Relmonitor.Models;

namespace Relmonitor.IRepositories
{
    public interface IStatus
    {
        Task<IEnumerable<Status>> Get();
        Task<Status> GetByDen(string den);
        Task<Status> GetById(int id);
        Task<Status> Create(Status status);
        Task Update(Status status);
        Task Delete(int id);
    }
}
