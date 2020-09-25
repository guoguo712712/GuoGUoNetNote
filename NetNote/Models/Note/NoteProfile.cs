using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using entity=guoguo.Domain.Entity;

namespace NetNote.Models.Note
{
    public class NoteProfile:Profile
    {
        public NoteProfile()
        {

            CreateMap<entity.Note, NoteModel>().ForMember(nn => nn.Attachment, mm => mm.Ignore());
            CreateMap<NoteModel, entity.Note>().ForMember(mm => mm.Attachment, nn => nn.Ignore()); 
        }
    }
}
