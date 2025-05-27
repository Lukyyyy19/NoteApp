namespace NotesAppWebAPI.Models;

public class Tag
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<Note> Notes { get; set; }  
}

public class TagCreateDTO
{
    public string Name { get; set; }
    
    public TagCreateDTO(){}

    public TagCreateDTO(Tag tag)
    {
        Name = tag.Name;
    }
}