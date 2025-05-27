using NotesAppWebAPI.Models;

namespace NotesAppWebAPI;

public interface INoteRepository
{
    Task<IEnumerable<Note>> GetAllNotesAsync();
    Task<Note?> GetNoteByIdAsync(int id);
    Task<Note> CreateNoteAsync(Note note);
    Task<Note?> UpdateNoteAsync(int id, Note note);
    Task<bool> DeleteNoteAsync(int id);
}