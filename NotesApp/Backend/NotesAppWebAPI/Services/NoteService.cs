using NotesAppWebAPI.Models;

namespace NotesAppWebAPI.Services;

public class NoteService:INoteService
{
    private readonly INoteRepository _noteRepository;
    public NoteService(INoteRepository noteRepository)
    {
        _noteRepository = noteRepository;
    }
    public async Task<IEnumerable<Note>> GetAllNotesAsync()
    {
        return await _noteRepository.GetAllNotesAsync();
    }
    
    public async Task<Note?> GetNoteByIdAsync(int id)
    {
        return await _noteRepository.GetNoteByIdAsync(id);
    }
    
    public async Task<Note> CreateNoteAsync(NoteCreateDTO note)
    {
        Note newNote = new Note
        {
            Title = note.Title,
            Content = note.Content,
            TagId = note.TagId,
        };
        if (note.TagId == null)
        {
            newNote.TagId = 2;
        }
        return await _noteRepository.CreateNoteAsync(newNote);
    }
    
    public async Task<Note?> UpdateNoteAsync(int id, NoteCreateDTO note)
    {
        Note? newNote = await GetNoteByIdAsync(id);
        if (newNote == null) return null;
        newNote.Title = note.Title;
        newNote.Content = note.Content;
        newNote.TagId = note.TagId;
        return await _noteRepository.UpdateNoteAsync(id, newNote);
    }
    
    public async Task<bool> DeleteNoteAsync(int id)
    {
        return await _noteRepository.DeleteNoteAsync(id);
    }
}