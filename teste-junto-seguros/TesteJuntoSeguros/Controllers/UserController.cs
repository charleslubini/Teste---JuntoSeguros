
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
using TesteJuntoSeguros.Models;
using TesteJuntoSeguros.Utils;

namespace TesteJuntoSeguros.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]

    public class UserController : ControllerBase 
    {

        private readonly UserContext _context;

        public UserController(UserContext context)
        {
            _context = context;
        }

        [HttpGet, Authorize]
        public async Task<ActionResult<IEnumerable<UserModel>>> GetUser()
        {
            return await _context.users.ToListAsync();
        }

        [HttpGet("{id}"), Authorize]
        public async Task<ActionResult<UserModel>> GetUser(long id)
        {
            var user = await _context.users.FindAsync(id);

            if(user == null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpPost, Authorize]
        public async Task<ActionResult<UserModel>> PostUser([FromBody] UserModel user)
        {
            var password = RandomGenerator.CalculateHash(user.password);
            user.password = password;

            _context.users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUser), new {id = user.id}, user); 
        }

        [HttpPut("{id}"), Authorize]
        public async Task<IActionResult> PutUser([FromBody] UserModel user, long id)
        {
            if(id != user.id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }
        
        [HttpDelete("{id}"), Authorize]
        public async Task<IActionResult> DeleteUser(long id)
        {
            var user = await _context.users.FindAsync(id);

            if (user == null)
            {
                NotFound();
            }

            _context.users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }

}