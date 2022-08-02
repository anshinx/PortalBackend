using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PortalBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        [HttpPost("/communitypost")]
        public ActionResult SharePost(PortalBackend.Objects.CommunityPost.PostDTO post)
        {

            return Ok("OK");
        }
    }
}
