using Microsoft.AspNetCore.Mvc;
using NotesAppWebAPI.Models;
using NotesAppWebAPI.Services;

namespace NotesAppWebAPI.Controllers;

public class TagController:ControllerBase
{
    private ITagService _tagService;
    public TagController( ITagService tagService)
    {
        _tagService = tagService;
    }

    [HttpGet]
    [Route("api/tag")]
    public async Task<IActionResult> GetAllTags()
    {
        var tags = await _tagService.GetAllTagsAsync();
        return Ok(tags);
    }

    [HttpGet]
    [Route("api/tag/id")]
    public async Task<IActionResult> GetTagById(int id)
    {
        var tag = await _tagService.GetTagByIdAsync(id);
        if (tag != null)
        {
            return Ok(tag);
        }

        return NoContent();
    }
    [HttpPost]
    [Route("api/tags")]
    public async Task<IActionResult> AddNote(TagCreateDTO tag)
    {
        Tag newTag = new Tag
        {
            Name = tag.Name,
        };
        
        await _tagService.CreateTagAsync(newTag);
        return Ok(newTag);
    }

    [HttpDelete]
    [Route("api/tags/id")]
    public async Task<IActionResult> DeleteTag(int id)
    {
        await _tagService.DeleteTagAsync(id);
        return Ok();
    }

    [HttpPut]
    [Route("api/tags/id")]
    public async Task<IActionResult> UpdateTag(int id,TagCreateDTO inputTag)
    {
        Tag newTag = await _tagService.GetTagByIdAsync(id);
        if (newTag == null) return NotFound();
        newTag.Name = inputTag.Name;
        await _tagService.UpdateTagAsync(id, newTag);
        return Ok();
    }
}