using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GoodConvo.Models;
using GoodConvo.Models.EntityModels;
using Microsoft.AspNetCore.Authorization;

namespace GoodConvo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CoachesController : ControllerBase
    {
        private readonly JournalContext _context;

        public CoachesController(JournalContext context)
        {
            _context = context;
        }

        // GET: api/Coaches
        [HttpGet]
        public IEnumerable<Coach> GetCoaches()
        {
            return _context.Coaches;
        }

        // GET: api/Coaches/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCoach([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var coach = await _context.Coaches.FindAsync(id);

            if (coach == null)
            {
                return NotFound();
            }

            return Ok(coach);
        }

        // PUT: api/Coaches/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCoach([FromRoute] int id, [FromBody] Coach coach)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != coach.Id)
            {
                return BadRequest();
            }

            _context.Entry(coach).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CoachExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Coaches
        [HttpPost]
        public async Task<IActionResult> PostCoach([FromBody] Coach coach)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Coaches.Add(coach);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCoach", new { id = coach.Id }, coach);
        }

        // DELETE: api/Coaches/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCoach([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var coach = await _context.Coaches.FindAsync(id);
            if (coach == null)
            {
                return NotFound();
            }

            _context.Coaches.Remove(coach);
            await _context.SaveChangesAsync();

            return Ok(coach);
        }

        private bool CoachExists(int id)
        {
            return _context.Coaches.Any(e => e.Id == id);
        }
    }
}