using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NotesAppWebAPI.Models;

public class Note
{
    public int Id { get; set; }
    public string? Title { get; set; }
    [Required] public string Content { get; set; }
    public int? TagId { get; set; }
    [JsonIgnore]public Tag? Tag { get; set; }
}

public class NoteCreateDTO
{
    public string? Title { get; set; }
    [Required] public string Content { get; set; }
    public int? TagId { get; set; }

    public NoteCreateDTO()
    {
    }

    public NoteCreateDTO(Note note)
    {
        Title = note.Title;
        Content = note.Content;
        TagId = note.TagId;
    }
}