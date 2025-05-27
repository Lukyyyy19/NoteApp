using Microsoft.EntityFrameworkCore;
using NotesAppWebAPI.Data;
using NotesAppWebAPI.Models;

namespace NotesAppWebAPI;

public class NoteRepository : INoteRepository
{
    private readonly NoteAppDbContext _context;

    public NoteRepository(NoteAppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Note>> GetAllNotesAsync()
    {
        return await _context.Notes.ToListAsync();
    }

    public async Task<Note?> GetNoteByIdAsync(int id)
    {
        return await _context.Notes.FirstOrDefaultAsync(n => n.Id == id);
    }

    public async Task<Note> CreateNoteAsync(Note note)
    {
        await _context.Notes.AddAsync(note);
        await _context.SaveChangesAsync();
        return note;
    }

    public async Task<Note?> UpdateNoteAsync(int id, Note note)
    {
        var noteToEdit = await _context.Notes.FirstOrDefaultAsync(x=> x.Id == id);
        noteToEdit = note;
        await _context.SaveChangesAsync();
        return noteToEdit;
    }

    public async Task<bool> DeleteNoteAsync(int id)
    {
        var noteToDelete = await _context.Notes.FirstOrDefaultAsync(x=> x.Id == id);
        _context.Notes.Remove(noteToDelete);
        return await _context.SaveChangesAsync() > 0;
    }
}