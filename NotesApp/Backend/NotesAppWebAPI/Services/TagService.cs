using NotesAppWebAPI.Models;

namespace NotesAppWebAPI.Services;

public class TagService: ITagService
{
    readonly ITagRepository _tagRepository;

    public TagService(ITagRepository tagRepository)
    {
        _tagRepository = tagRepository;
    }
    public async Task<IEnumerable<Tag>> GetAllTagsAsync()
    {
        return await _tagRepository.GetAllTagsAsync();
    }

    public async Task<Tag?> GetTagByIdAsync(int id)
    {
        return await _tagRepository.GetTagByIdAsync(id);
    }

    public async Task<Tag> CreateTagAsync(Tag tag)
    {
        return await _tagRepository.CreateTagAsync(tag);
    }

    public async Task<Tag?> UpdateTagAsync(int id, Tag tag)
    {
        return await _tagRepository.UpdateTagAsync(id, tag);
    }

    public async Task<bool> DeleteTagAsync(int id)
    {
        return await _tagRepository.DeleteTagAsync(id);
    }
}