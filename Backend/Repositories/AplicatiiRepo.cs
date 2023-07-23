using Microsoft.EntityFrameworkCore;
using Relmonitor.IRepositories;
using Relmonitor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Relmonitor.Repositories
{
    public class AplicatiiRepo : IAplicatii
    {
        private readonly postgresContext _postgresContext;

        public AplicatiiRepo(postgresContext context)
        {
            _postgresContext = context;
        }

        public async Task<Aplicatii> Create(Aplicatii app)
        {
            _postgresContext.Aplicatii.Add(app);
            await _postgresContext.SaveChangesAsync();

            return app;
        }

        public async Task Delete(int id)
        {
            var appToDelete = await _postgresContext.Aplicatii.FindAsync(id);
            _postgresContext.Aplicatii.Remove(appToDelete);
            await _postgresContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Aplicatii>> Get()
        {
            return await _postgresContext.Aplicatii.Include(_ => _.Releases).OrderBy(x => x.Idaplicatie).ToListAsync();
        }

        public async Task<Aplicatii> GetByDen(string den)
        {
            return await _postgresContext.Aplicatii.Include(_ => _.Releases).Where(x => x.Denumire.Equals(den)).SingleOrDefaultAsync();
        }

        public async Task<Aplicatii> GetById(int id)
        {
            return await _postgresContext.Aplicatii.FindAsync(id);
        }

        public async Task Update(Aplicatii com)
        {
            _postgresContext.Entry(com).State = EntityState.Modified;
            await _postgresContext.SaveChangesAsync();
        }
    }
}
