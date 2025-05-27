using NotesAppWebAPI.Models;

namespace NotesAppWebAPI.Services;

public interface ITagService
{
    Task<IEnumerable<Tag>> GetAllTagsAsync();
    Task<Tag?> GetTagByIdAsync(int id);
    Task<Tag> CreateTagAsync(Tag note);
    Task<Tag?> UpdateTagAsync(int id, Tag note);
    Task<bool> DeleteTagAsync(int id);
}