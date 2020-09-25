using guoguo.Domain.Entity;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NetNote.Models.Note
{
    public class NoteModel
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "请输入标题"), Display(Name = "标题")]
        public string Title { get; set; }
        [Required(ErrorMessage = "请输入内容"), Display(Name = "内容")]
        public string Content { get; set; }
        [Display(Name = "类型")]
        public int NoteTypeID { get; set; }

        [Display(Name = "类型")]
        public NoteType Type { get; set; }

        [Display(Name ="密码")]
        public string Password { get; set; }

        [Display(Name ="附件")]
        public IFormFile Attachment { get; set; }

        [Display(Name = "操作人")]
        public string OpUser { get; set; }
        [Display(Name = "操作时间")]
        public DateTime OpTime { get; set; }
    }
}
