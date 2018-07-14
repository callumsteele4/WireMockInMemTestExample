using System.Threading.Tasks;
using Api.Clients.Users;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UsersClient _usersClient;

        public UsersController(UsersClient usersClient)
        {
            _usersClient = usersClient;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(
                await _usersClient.GetUserById(id));
        }
    }
}