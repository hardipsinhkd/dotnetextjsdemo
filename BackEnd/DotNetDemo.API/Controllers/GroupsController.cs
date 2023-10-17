using DotNetDemo.Service;
using DotNetDemo.Service.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace DotNetDemo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        private readonly IGroupService _groupService;
        public GroupsController(IGroupService groupService)
        {
            _groupService = groupService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _groupService.GetAll();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _groupService.Get(id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateGroupDto input)
        {
            var affectedRowCounts =  await _groupService.Create(input);

            if (affectedRowCounts <= 0)
            {
                return BadRequest("Something wrong happened!");
            }
            return Ok("Group added successfully!");
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateGroupDto input)
        {
            var affectedRowCounts = await _groupService.Update(input);
            if (affectedRowCounts <= 0)
            {
                return BadRequest("Something wrong happened!");
            }
            return Ok("Group updated Successfully");

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var affectedRowCounts = await _groupService.Delete(id);
            if (affectedRowCounts <= 0)
            {
                return BadRequest("Something wrong happened!");
            }
            return Ok("Group deleted successfully!");
        }

        [HttpGet("GetAllForAssigning")]
        public async Task<IActionResult> GetAllForAssigning()
        {
            var result = await _groupService.GetAllForAssigning();
            return Ok(result);
        }
    }
}
