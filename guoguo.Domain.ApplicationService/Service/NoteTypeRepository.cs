using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using guoguo.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace guoguo.Domain.ApplicationService
{
    public class NoteTypeRepository : INoteTypeRepository
    {
        private readonly NoteDbContext _NoteDbContext;
        public NoteTypeRepository(NoteDbContext noteDbContext)
        {
            _NoteDbContext = noteDbContext;
        }

        public Task<List<NoteType>> ListAsync()
        {
            return _NoteDbContext.NoteTypes.ToListAsync();
        }
    }
}
