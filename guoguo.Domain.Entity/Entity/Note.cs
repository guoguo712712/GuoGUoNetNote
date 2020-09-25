using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace guoguo.Domain.Entity
{
    public class Note
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string Password { get; set; }

        public string Attachment { get; set; }

        public string OpUser { get; set; }

        public DateTime OpTime { get; set; }

        public int NoteTypeID { get; set; }

        public virtual NoteType Type{ get; set; }
    }

    internal class NoteConfiguration : IEntityTypeConfiguration<Note>
    {
        public void Configure(EntityTypeBuilder<Note> builder)
        {
            builder.HasKey(ee => ee.ID);
            builder.ToTable<Note>("Notes");  

            builder.Property(ee => ee.Title).IsRequired().HasMaxLength(100);
            builder.Property(ee => ee.Content).IsRequired();
            builder.Property(ee => ee.OpUser).IsRequired();
            builder.Property(ee => ee.OpTime).IsRequired();

            
        }
    }

}
