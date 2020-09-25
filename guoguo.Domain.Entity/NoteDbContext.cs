using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace guoguo.Domain.Entity
{
    public class NoteDbContext:IdentityDbContext<NoteUser>//:DbContext
    {
        public NoteDbContext(DbContextOptions options):base(options)
        {

        }

        public DbSet<Note> Notes { get; set; }

        public DbSet<NoteType> NoteTypes { get; set; }

        public DbSet<BasicUser> BasicUsers { get; set; }

        public DbSet<NoteUser> NoteUsers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           // optionsBuilder.UseSqlServer("Server=10.10.31.186;DataBase=NetNoteTest;Uid=sa;pwd=Admin2012;");
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration<Note>(new NoteConfiguration())
                .ApplyConfiguration<NoteType>(new NoteTypeConfiguration());
                //.ApplyConfiguration<NoteUser>(new NoteUserConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
