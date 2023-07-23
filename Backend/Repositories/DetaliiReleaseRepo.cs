using Microsoft.EntityFrameworkCore;
using Relmonitor.IRepositories;
using Relmonitor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Relmonitor.Repositories
{
    public class DetaliiReleaseRepo : IDetaliiRelease
    {
        private readonly postgresContext _postgresContext;

        public DetaliiReleaseRepo(postgresContext context)
        {
            _postgresContext = context;
        }
        public async Task<Detaliirelease> Create(Detaliirelease detalii)
        {
            _postgresContext.Detaliirelease.Add(detalii);
            await _postgresContext.SaveChangesAsync();

            return detalii;
        }

        public async Task Delete(int id)
        {
            var viewToDelete = await _postgresContext.Detaliirelease.FindAsync(id);
            _postgresContext.Detaliirelease.Remove(viewToDelete);
            await _postgresContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Detaliirelease>> Get()
        {
            return await _postgresContext.Detaliirelease.OrderBy(x => x.Idrelease).ToListAsync();
        }

        public async Task<IEnumerable<Detaliirelease>> GetByDen(string den)
        {
            return await _postgresContext.Detaliirelease.Where(x => x.Codaplicatie.Equals(den)).ToListAsync();
        }

        public async Task<Detaliirelease> GetById(int id)
        {
            return await _postgresContext.Detaliirelease.Where(x => x.Idrelease.Equals(id)).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Detaliirelease>> GetMatchDate(DateTime date)
        {
            return await _postgresContext.Detaliirelease.Where(x => x.Datarelease.Value.Date == date.Date).ToListAsync();
        }

        public async Task Update(Detaliirelease detalii)
        {
            _postgresContext.Entry(detalii).State = EntityState.Modified;
            await _postgresContext.SaveChangesAsync();
        }
    }
}
