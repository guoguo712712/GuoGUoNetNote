using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace guoguo.Domain.Entity
{
   public class NoteType
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public List<Note> Notes { get; set; }
    }

    internal class NoteTypeConfiguration : IEntityTypeConfiguration<NoteType>
    {
        public void Configure(EntityTypeBuilder<NoteType> builder)
        {
            builder.ToTable("NoteTypes");

            builder.HasKey(tt => tt.ID);

            builder.Property(tt => tt.Name).HasMaxLength(64);

            
        }
    }
}
