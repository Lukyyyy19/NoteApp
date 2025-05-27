using NotesAppWebAPI.Models;

namespace NotesAppWebAPI.Services;

public interface INoteService
{
    Task<IEnumerable<Note>> GetAllNotesAsync();
    Task<Note?> GetNoteByIdAsync(int id);
    Task<Note> CreateNoteAsync(NoteCreateDTO note);
    Task<Note?> UpdateNoteAsync(int id, NoteCreateDTO note);
    Task<bool> DeleteNoteAsync(int id);
}