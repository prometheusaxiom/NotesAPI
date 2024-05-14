using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotesAPI.Data;

namespace NotesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly DataContext _context;

        public NotesController(DataContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<List<Notes>>> GetNotes()
        {
            return Ok(await _context.Notes.ToListAsync());
        }

        [HttpPost]
        public async Task<ActionResult<List<Notes>>> CreateNote(Notes note)
        {
            _context.Notes.Add(note);
            await _context.SaveChangesAsync();

            return Ok(await _context.Notes.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<Notes>>> UpdateNote(Notes note)
        {
            var dbNote = await _context.Notes.FindAsync(note.Id);
            if(dbNote == null)
            {
                return BadRequest("Note not found.");
            }

            dbNote.Title = note.Title;
            dbNote.Content = note.Content;
            dbNote.Author = note.Author;

            await _context.SaveChangesAsync();

            return Ok(await _context.Notes.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Notes>>> DeleteNote(int id)
        {
            var dbNote = await _context.Notes.FindAsync(id);
            if(dbNote == null)
            {
                return BadRequest("Note not found.");
            }

            _context.Notes.Remove(dbNote);
            await _context.SaveChangesAsync();

            return Ok(await _context.Notes.ToListAsync());
        }
    }
}
