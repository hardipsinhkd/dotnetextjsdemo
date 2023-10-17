using DotNetDemo.Service.Dtos;
using DotNetDemo.Service;
using Microsoft.AspNetCore.Mvc;

namespace DotNetDemo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _userService.GetAll();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _userService.Get(id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserDto input)
        {
            var affectedRowCounts = await _userService.Create(input);

            if (affectedRowCounts <= 0)
            {
                return BadRequest("Something wrong happened!");
            }
            return Ok("User added successfully!");
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateUserDto input)
        {
            var affectedRowCounts = await _userService.Update(input);
            if (affectedRowCounts <= 0)
            {
                return BadRequest("Something wrong happened!");
            }
            return Ok("User updated successfully");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var affectedRowCounts = await _userService.Delete(id);
            if (affectedRowCounts <= 0)
            {
                return BadRequest("Something wrong happened!");
            }
            return Ok("User deleted successfully!");
        }

        [HttpPost("AssignGroup")]
        public async Task<IActionResult> AssignGroup(AssignGroupToUserDto input)
        {
            var affectedRowCounts = await _userService.AssignGroup(input);
            if (affectedRowCounts <= 0)
            {
                return BadRequest("Something wrong happened!");
            }
            return Ok("Group assigned successfully!");
        }
    }
}
