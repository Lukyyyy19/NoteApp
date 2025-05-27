using NotesAppWebAPI.Models;

namespace NotesAppWebAPI;

public interface ITagRepository
{
    Task<IEnumerable<Tag>> GetAllTagsAsync();
    Task<Tag?> GetTagByIdAsync(int id);
    Task<Tag> CreateTagAsync(Tag note);
    Task<Tag?> UpdateTagAsync(int id, Tag note);
    Task<bool> DeleteTagAsync(int id);
}