using Microsoft.EntityFrameworkCore;
using NotesAppWebAPI.Data;
using NotesAppWebAPI.Models;

namespace NotesAppWebAPI;

public class TagRepository : ITagRepository
{
    private NoteAppDbContext _context;

    public TagRepository(NoteAppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Tag>> GetAllTagsAsync()
    {
        return await _context.Tags.ToListAsync();
    }

    public async Task<Tag?> GetTagByIdAsync(int id)
    {
        return await _context.Tags.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Tag> CreateTagAsync(Tag tag)
    {
        await _context.Tags.AddAsync(tag);
        await _context.SaveChangesAsync();
        return tag;
    }

    public async Task<Tag?> UpdateTagAsync(int id, Tag tag)
    {
        var tagToUpdate = await _context.Tags.FirstOrDefaultAsync(x => x.Id == id);
        tagToUpdate.Name = tag.Name;
        await _context.SaveChangesAsync();
        return tagToUpdate;
    }

    public async Task<bool> DeleteTagAsync(int id)
    {
        var tagToDelete = await _context.Tags.FirstOrDefaultAsync(x => x.Id == id);
        _context.Tags.Remove(tagToDelete);
        return await _context.SaveChangesAsync() > 0;
    }
}