using Microsoft.EntityFrameworkCore;
using Relmonitor.IRepositories;
using Relmonitor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Relmonitor.Repositories
{
    public class UtilizatoriRepo : IUtilizatori
    {
        private readonly postgresContext _postgresContext;

        public UtilizatoriRepo(postgresContext context)
        {
            _postgresContext = context;
        }
        public async Task<Utilizatori> Create(Utilizatori utiilizatori)
        {
            _postgresContext.Utilizatori.Add(utiilizatori);
            await _postgresContext.SaveChangesAsync();

            return utiilizatori;
        }

        public async Task Delete(int id)
        {
            var utilToDelete = await _postgresContext.Utilizatori.FindAsync(id);
            _postgresContext.Utilizatori.Remove(utilToDelete);
            await _postgresContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Utilizatori>> Get()
        {
            return await _postgresContext.Utilizatori.Include(_ => _.Releases).OrderBy(x => x.Idutilizator).ToListAsync();
        }

        public async Task<Utilizatori> GetByDen(string den)
        {
            return await _postgresContext.Utilizatori.Include(_ => _.Releases).Where(x => x.Email == den).SingleOrDefaultAsync();
        }

        public async Task<Utilizatori> GetById(int id)
        {
            return await _postgresContext.Utilizatori.FindAsync(id);
        }

        public async Task Update(Utilizatori utiilizatori)
        {
            _postgresContext.Entry(utiilizatori).State = EntityState.Modified;
            await _postgresContext.SaveChangesAsync();
        }
    }
}
