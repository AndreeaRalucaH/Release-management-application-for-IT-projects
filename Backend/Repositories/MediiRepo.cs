using Microsoft.EntityFrameworkCore;
using Relmonitor.IRepositories;
using Relmonitor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Relmonitor.Repositories
{
    public class MediiRepo: IMedii
    {
        private readonly postgresContext _postgresContext;

        public MediiRepo(postgresContext context)
        {
            _postgresContext = context;
        }

        public async Task<Medii> Create(Medii medii)
        {
            _postgresContext.Medii.Add(medii);
            await _postgresContext.SaveChangesAsync();

            return medii;
        }

        public async Task Delete(int id)
        {
            var mediuToDelete = await _postgresContext.Medii.FindAsync(id);
            _postgresContext.Medii.Remove(mediuToDelete);
            await _postgresContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Medii>> Get()
        {
            return await _postgresContext.Medii.Include(_ => _.Releases).OrderBy(x => x.Idmediu).ToListAsync();
        }

        public async Task<Medii> GetByDen(string den)
        {
            return await _postgresContext.Medii.Include(_ => _.Releases).Where(x => x.Denumire.Equals(den)).SingleOrDefaultAsync();
        }

        public async Task<Medii> GetById(int id)
        {
            return await _postgresContext.Medii.FindAsync(id);
        }

        public async Task Update(Medii medii)
        {
            _postgresContext.Entry(medii).State = EntityState.Modified;
            await _postgresContext.SaveChangesAsync();
        }
    }
}
