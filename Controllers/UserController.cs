using Microsoft.AspNetCore.Mvc;
using MTCBackend.Interfaces;
using MTCBackend.Models;

namespace MTCBackend.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/user
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers([FromQuery] UserType? userType)
        {
            if (userType.HasValue)
            {
                if (!Enum.IsDefined(typeof(UserType), userType.Value))
                {
                    return BadRequest("Invalid user type.");
                }

                var filteredUsers = await _userService.GetUsersByType(userType.Value);
                return Ok(filteredUsers);
            }

            var users = await _userService.GetAllUsers();
            return Ok(users);
        }

        // GET: api/user/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _userService.GetUserById(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // POST: api/user
        [HttpPost]
        public async Task<ActionResult<User>> AddUser(User user)
        {
            var createdUser = await _userService.AddUser(user);
            return CreatedAtAction(nameof(GetUser), new { id = createdUser.Id }, createdUser);
        }

        // PUT: api/user/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(Guid id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            var result = await _userService.UpdateUser(user);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/user/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var result = await _userService.DeleteUser(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }

}
