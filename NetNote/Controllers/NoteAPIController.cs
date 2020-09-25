using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using guoguo.Domain.ApplicationService;
using guoguo.Domain.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetNote.Models.Note;

namespace NetNote.Controllers
{
    

    [Route("api/[controller]")]
    [ApiController]
    public class NoteAPIController :ControllerBase
    {
        private IMapper _Mapper;

        private readonly INoteRepository _NoteRepository;
        private readonly INoteTypeRepository _NoteTypeRepository;

        public NoteAPIController(IServiceProvider serviceProvider, INoteRepository noteRepository, INoteTypeRepository noteTypeRepository, IMapper mapper)
        {
            _NoteRepository = noteRepository;
            _NoteTypeRepository = noteTypeRepository;

            _Mapper = mapper;

            serviceProvider.GetService(typeof(NoteDbContext));

        }
        /// <summary>
        /// 根据页码获取数据
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        [HttpGet("{pageIndex}")]
        public IActionResult Index(int pageIndex = 1)
        {
            int pageSize = 5;

            if (pageIndex < 1)
                pageIndex = 1;          

            Tuple<List<Note>, int> tuple = _NoteRepository.PageList(pageIndex, pageSize);
            return Ok(tuple.Item1);
        }

       

        //public async Task<IActionResult> Details(int id)
        //{
        //    Note note = await _NoteRepository.GetByIdAsync(id);
        //    NoteModel model = _Mapper.Map<NoteModel>(note);
        //    if (note.Attachment != null)
        //        note.Attachment = "/" + note.Attachment;

        //    return Ok(model);
        //}

        

    }
}