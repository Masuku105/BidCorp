using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserApi.UserApIDbAccess;
using UserApi.UserApiDtos;
using UserApi.UserApiModels;

namespace UserApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserContext _context;

        public UsersController(UserContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            var user = (await _context.Users.ToListAsync());
            var dtoUsers = user.Select(user => user.AsDto(user.Id, user.Name, user.Surname)).ToList();
            return Ok(user);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetProduct(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return user.AsDto(user.Id, user.Name,user.Surname);
        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> PostUser(UserDto user)
        {
            
            _context.Users.Add(user.AsEntity(user.id,user.name, user.surname));
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetUsers), user);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, UserDto user)
        {
            if (id != user.id)
            {
                return BadRequest();
            }
          
            _context.Entry(user.AsEntity(user.id,user.name,user.surname)).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
