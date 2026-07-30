using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentResultAppMvcAndApi.Data;
using StudentResultAppMvcAndApi.Models.Entities;

namespace StudentResultAppMvcAndApi.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    [ApiController]
    public class ResultApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ResultApiController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Result>>> GetResult()
        {
            return await _context.results.ToListAsync();
        }
        [HttpPost]
        public async Task<IActionResult> PostResult(Result result)
        {
            if (result.StudentId != null)
            {
                return BadRequest("StudentId is required");
            }

            var studentExists = await _context.students.AnyAsync(s => s.StudentId == result.StudentId);
            if (!studentExists)
            {
                return BadRequest($"Student with ID {result.StudentId} does not exist.");
            }

            try
            {
                _context.results.Add(result);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetResult), new { id = result.ResultId }, result);
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, $"Database error: {ex.InnerException?.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
