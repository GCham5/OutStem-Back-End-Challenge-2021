using Frank_Workshop.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Frank_Workshop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User newUser)
        {

            User user = new User
            {
                FirstName = newUser.FirstName,
                LastName = newUser.LastName,
                Email = newUser.Email,
                IsPremium = false
            };

             _context.Add(user);
            await _context.SaveChangesAsync();

            return StatusCode(StatusCodes.Status201Created);

        }

        [HttpPut("premium")]
        public async Task<IActionResult> Premium([FromBody] User user)
        {
            var existingUser = await _context.User.FindAsync(user.Id);

            if (existingUser == null)
            {
                return NotFound("User not found");
            }

            if (existingUser.IsPremium)
            {
                return BadRequest("User is already premium");
            }

            existingUser.IsPremium = true;
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
