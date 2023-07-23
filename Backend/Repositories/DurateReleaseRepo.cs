using Microsoft.EntityFrameworkCore;
using Relmonitor.IRepositories;
using Relmonitor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Relmonitor.Repositories
{
    public class DurateReleaseRepo : IDurateRelease
    {
        private readonly postgresContext _postgresContext;

        public DurateReleaseRepo(postgresContext context)
        {
            _postgresContext = context;
        }
        public async Task<Duraterelease> Create(Duraterelease durate)
        {
            _postgresContext.Duraterelease.Add(durate);
            await _postgresContext.SaveChangesAsync();

            return durate;
        }

        public async Task Delete(int id)
        {
            var durataToDelete = await _postgresContext.Duraterelease.FindAsync(id);
            _postgresContext.Duraterelease.Remove(durataToDelete);
            await _postgresContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Duraterelease>> Get()
        {
            return await _postgresContext.Duraterelease.Include(_ => _.Releases).OrderBy(x => x.Iddurata).ToListAsync();
        }

        public Task<Duraterelease> GetByDen(string den)
        {
            throw new NotImplementedException();
        }

        public async Task<Duraterelease> GetById(int id)
        {
            return await _postgresContext.Duraterelease.FindAsync(id);
        }

        public async Task Update(Duraterelease durate)
        {
            _postgresContext.Entry(durate).State = EntityState.Modified;
            await _postgresContext.SaveChangesAsync();
        }
    }
}
