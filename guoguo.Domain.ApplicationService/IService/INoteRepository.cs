using guoguo.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace guoguo.Domain.ApplicationService
{
  public  interface INoteRepository
    {
        Task<Note> GetByIdAsync(int id);

        Task<List<Note>> ListAsync();

        Tuple<List<Note>, int> PageList(int pageIndex, int pageSize);

        Task AddAsync(Note note);

        Task UpdateAsync(Note note);

        Task DeleteAsync(int id);

    }
}
