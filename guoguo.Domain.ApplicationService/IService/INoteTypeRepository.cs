using guoguo.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace guoguo.Domain.ApplicationService
{
  public  interface INoteTypeRepository
    {
        Task<List<NoteType>> ListAsync();
    }
}
