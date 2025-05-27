using Microsoft.AspNetCore.Mvc;
using NotesAppWebAPI.Models;
using NotesAppWebAPI.Services;

namespace NotesAppWebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NoteController : ControllerBase
{
    private INoteService _noteService;
    private ITagService _tagService;

    public NoteController(INoteService noteService, ITagService tagService)
    {
        _noteService = noteService;
        _tagService = tagService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllNotes()
    {
        var notes = await _noteService.GetAllNotesAsync();
        return Ok(notes);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetNotesById(int id)
    {
        var note = await _noteService.GetNoteByIdAsync(id);
        if (note != null)
        {
            return Ok(note);
        }

        return NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> AddNote([FromBody] NoteCreateDTO note)
    {
        await _noteService.CreateNoteAsync(note);
        return Ok(new { message = "Successfully created" });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteNote(int id)
    {
        await _noteService.DeleteNoteAsync(id);
        return Ok(new { message = "Successfully deleted"});
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateNote(int id, [FromBody] NoteCreateDTO inputNote)
    {
        var note = await _noteService.UpdateNoteAsync(id, inputNote);
        if (note != null)
            return Ok(new { message = "Successfully updated", Note = note });
        return NoContent();
    }
}