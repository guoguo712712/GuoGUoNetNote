using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace guoguo.Domain.Entity
{
   public class NoteUser:IdentityUser
    {
        //public int ID { get; set; }

    }


    internal class NoteUserConfiguration : IEntityTypeConfiguration<NoteUser>
    {
      

        public void Configure(EntityTypeBuilder<NoteUser> builder)
        {
            //builder.HasKey(uu => uu.ID);
        }
    }
}
