using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using guoguo.Domain.ApplicationService;
using guoguo.Domain.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NetNote.Models.Note;

namespace NetNote.Controllers
{
    [Authorize]
    public class NoteController : Controller
    {
        private IMapper _Mapper;

        private readonly INoteRepository _NoteRepository;
        private readonly INoteTypeRepository _NoteTypeRepository;

        public NoteController(INoteRepository noteRepository,INoteTypeRepository noteTypeRepository,IMapper mapper)
        {
            _NoteRepository = noteRepository;
            _NoteTypeRepository = noteTypeRepository;

            _Mapper = mapper;
        }

        public IActionResult Index(int pageIndex=1)
        {
            int pageSize = 5;

            if (pageIndex < 1)
                pageIndex = 1;
           


            //List<Note> noteList =await _NoteRepository.ListAsync();
            //List<NoteModel> modelList = noteList.ConvertAll(nn => _Mapper.Map<NoteModel>(nn));

            Tuple<List<Note>, int> tuple = _NoteRepository.PageList(pageIndex, pageSize);
            ViewBag.PageIndex = pageIndex;
            ViewBag.PageCount = tuple.Item2;

            List<Note> noteList = tuple.Item1;
            List<NoteModel> modelList = noteList.ConvertAll(nn => _Mapper.Map<NoteModel>(nn));

            return View(modelList);
        }

        public async Task<IActionResult> AddNote()
        {
            NoteModel model = new NoteModel();
            model.OpUser = "Admin";
            model.OpTime = DateTime.Now;

            List<NoteType> noteTypeList = await _NoteTypeRepository.ListAsync();
            ViewBag.Types = noteTypeList.ConvertAll(nt => new SelectListItem { Text = nt.Name, Value = nt.ID.ToString() });

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddNote([FromServices]IHostingEnvironment env, NoteModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);          

            Note note = _Mapper.Map<Note>(model);
            note.OpUser = "Admin";
            note.OpTime = DateTime.Now;

            string fileName = "";
            if (model.Attachment != null)
            {
                fileName = Path.Combine("file", Guid.NewGuid().ToString() + Path.GetExtension(model.Attachment.FileName));

                using (FileStream stream = new FileStream(Path.Combine(env.WebRootPath, fileName), FileMode.CreateNew))
                {
                    model.Attachment.CopyTo(stream);
                }
            }
            note.Attachment = fileName;

            await _NoteRepository.AddAsync(note);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> EditNote(int id)
        {
            Note note =await _NoteRepository.GetByIdAsync(id);
            NoteModel model = _Mapper.Map<NoteModel>(note);

            List<NoteType> noteTypeList = await _NoteTypeRepository.ListAsync();
            ViewBag.Types = noteTypeList.ConvertAll(nt => new SelectListItem { Text = nt.Name, Value = nt.ID.ToString() });
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditNote(NoteModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            Note note = _Mapper.Map<Note>(model);
            note.OpUser = "Admin";
            note.OpTime = DateTime.Now;
            await _NoteRepository.UpdateAsync(note);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int id)
        {
            Note note = await _NoteRepository.GetByIdAsync(id);
            NoteModel model = _Mapper.Map<NoteModel>(note);
            if (note.Attachment != null)
                ViewBag.File ="/"+note.Attachment;

            return View(model);
        }

        public async Task<IActionResult> DetailFile(int id,string password)
        {
            Note note = await _NoteRepository.GetByIdAsync(id);
            if(note.Password!=password)
            {
                return RedirectToAction("Details", id);
            }

            NoteModel model = _Mapper.Map<NoteModel>(note);
            
            return View(model);

        }

        public async Task<IActionResult> DeleteNote(int id)
        {
            Note note = await _NoteRepository.GetByIdAsync(id);
            NoteModel model = _Mapper.Map<NoteModel>(note);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteNote(NoteModel model)
        {
            await _NoteRepository.DeleteAsync(model.ID);
            return RedirectToAction("Index");
        }


    }
}