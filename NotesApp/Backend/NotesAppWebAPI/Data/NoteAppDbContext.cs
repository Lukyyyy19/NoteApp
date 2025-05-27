using Microsoft.EntityFrameworkCore;
using NotesAppWebAPI.Models;

namespace NotesAppWebAPI.Data;

public class NoteAppDbContext:DbContext
{
   public DbSet<Note> Notes { get;set; }
   public DbSet<Tag> Tags { get;set; }
   public NoteAppDbContext(DbContextOptions<NoteAppDbContext> options) : base(options)
   {
   }

   protected override void OnModelCreating(ModelBuilder modelBuilder)
   {
      modelBuilder.Entity<Tag>()
         .HasMany(n => n.Notes)
         .WithOne(n => n.Tag)
         .HasForeignKey(n => n.TagId)
         .HasPrincipalKey(p => p.Id)
         .OnDelete(DeleteBehavior.Cascade);
   }
}