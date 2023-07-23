using Microsoft.EntityFrameworkCore;
using Relmonitor.IRepositories;
using Relmonitor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Relmonitor.Repositories
{
    public class ImpulseRepo : IImpulse
    {
        private readonly postgresContext _postgresContext;

        public ImpulseRepo(postgresContext context)
        {
            _postgresContext = context;
        }
        public async Task<Impulse> Create(Impulse impulse)
        {
            _postgresContext.Impulse.Add(impulse);
            await _postgresContext.SaveChangesAsync();

            return impulse;
        }

        public async Task Delete(int id)
        {
            var impulseToDelete = await _postgresContext.Impulse.FindAsync(id);
            _postgresContext.Impulse.Remove(impulseToDelete);
            await _postgresContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Impulse>> Get()
        {
            return await _postgresContext.Impulse.OrderBy(x => x.Idrelease).ToListAsync();
        }

        public Task<Impulse> GetByDen(string den)
        {
            throw new NotImplementedException();
        }

        public async Task<Impulse> GetById(int id)
        {
            return await _postgresContext.Impulse.FindAsync(id);
        }

        public async Task Update(Impulse impulse)
        {
            _postgresContext.Entry(impulse).State = EntityState.Modified;
            await _postgresContext.SaveChangesAsync();
        }
    }
}
