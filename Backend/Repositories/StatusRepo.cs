using Microsoft.EntityFrameworkCore;
using Relmonitor.IRepositories;
using Relmonitor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Relmonitor.Repositories
{
    public class StatusRepo : IStatus
    {
        private readonly postgresContext _postgresContext;

        public StatusRepo(postgresContext context)
        {
            _postgresContext = context;
        }
        public async Task<Status> Create(Status status)
        {
            _postgresContext.Status.Add(status);
            await _postgresContext.SaveChangesAsync();

            return status;
        }

        public async Task Delete(int id)
        {
            var statusToDelete = await _postgresContext.Status.FindAsync(id);
            _postgresContext.Status.Remove(statusToDelete);
            await _postgresContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Status>> Get()
        {
            return await _postgresContext.Status.Include(_ => _.Releases).OrderBy(x => x.Idstatus).ToListAsync();
        }

        public async Task<Status> GetByDen(string den)
        {
            return await _postgresContext.Status.Include(_ => _.Releases).Where(x => x.Denumire.Equals(den)).SingleOrDefaultAsync();
        }

        public async Task<Status> GetById(int id)
        {
            return await _postgresContext.Status.FindAsync(id);
        }

        public async Task Update(Status status)
        {
            _postgresContext.Entry(status).State = EntityState.Modified;
            await _postgresContext.SaveChangesAsync();
        }
    }
}
