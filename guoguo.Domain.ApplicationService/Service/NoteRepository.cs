using guoguo.Domain.Entity;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace guoguo.Domain.ApplicationService
{
    public class NoteRepository : INoteRepository
    {
        private NoteDbContext _NoteContext;
        public NoteRepository(NoteDbContext context)
        {
            _NoteContext = context;
            
        }

        public Task<Note> GetByIdAsync(int id)
        {
            return Task.Run(() =>
            {
                Note note = _NoteContext.Notes.Include(nn => nn.Type).Where(nn => nn.ID == id).FirstOrDefault();               
                return note;
            });
        }

        public Task<List<Note>> ListAsync()
        {
            return _NoteContext.Notes.Include(nn => nn.Type).ToListAsync();
        }

        /// <summary>
        /// Tuple<List<Note>, PageCount>
        /// </summary>
        /// <param name="pageIndex">从1开始</param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public Tuple<List<Note>, int> PageList(int pageIndex, int pageSize)
        {
            if (_NoteContext.Notes.Count() <= 0)
                return new Tuple<List<Note>, int>(new List<Note>(), 1);


            int itemCount = _NoteContext.Notes.Include(nn => nn.Type).Count();
            int pageCount = itemCount % pageSize == 0 ? itemCount / pageSize : itemCount / pageSize + 1;

            int skipItemCount = (pageIndex-1)*pageSize;
            int takeItemCount = itemCount - skipItemCount > pageSize ? pageSize : itemCount - skipItemCount;
            List<Note> notelist =_NoteContext.Notes.Include(nn => nn.Type).OrderBy(nn => nn.OpTime).Skip(skipItemCount).Take(takeItemCount).ToList();

            return new Tuple<List<Note>, int>(notelist, pageCount);
        }

        public Task AddAsync(Note note)
        {
            _NoteContext.Notes.Add(note);
          return  _NoteContext.SaveChangesAsync();
        }

        public Task UpdateAsync(Note note)
        {
            _NoteContext.Entry(note).State = EntityState.Modified;
            return _NoteContext.SaveChangesAsync();
        }

        public Task DeleteAsync(int id)
        {
           return Task.Run(() =>
            {
                Note note = _NoteContext.Notes.Find(id);
                if (note != null)
                {
                    _NoteContext.Notes.Remove(note);
                    _NoteContext.SaveChanges();
                }
            });         

        }


    }
}
