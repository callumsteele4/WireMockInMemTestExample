using System.Threading.Tasks;
using Api.Clients.Posts;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly PostsClient _postsClient;

        public PostsController(PostsClient postsClient)
        {
            _postsClient = postsClient;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(
                await _postsClient.GetPostById(id));
        }
    }
}