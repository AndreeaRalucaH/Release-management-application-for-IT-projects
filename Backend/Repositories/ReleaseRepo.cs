using Microsoft.EntityFrameworkCore;
using Relmonitor.IRepositories;
using Relmonitor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Relmonitor.Repositories
{
    public class ReleaseRepo: IRelease
    {
        private readonly postgresContext _postgresContext;

        public ReleaseRepo(postgresContext context)
        {
            _postgresContext = context;
        }

        public async Task<Release> Create(Release release)
        {
            _postgresContext.Release.Add(release);
            await _postgresContext.SaveChangesAsync();

            return release;
        }

        public async Task Delete(int id)
        {
            var releaseToDelete = await _postgresContext.Release.FindAsync(id);
            _postgresContext.Release.Remove(releaseToDelete);
            await _postgresContext.SaveChangesAsync(); ;
        }

        public async Task<IEnumerable<Release>> Get()
        {
            return await _postgresContext.Release.OrderBy(x => x.Idrelease).ToListAsync();
        }

        public Task<Release> GetByDen(string den)
        {
            throw new NotImplementedException();
        }

        public async Task<Release> GetById(int id)
        {
            return await _postgresContext.Release.FindAsync(id);
        }

        public async Task Update(Release release)
        {
            _postgresContext.Entry(release).State = EntityState.Modified;
            await _postgresContext.SaveChangesAsync();
        }
    }
}
