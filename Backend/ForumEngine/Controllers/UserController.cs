using ForumEngine.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ForumEngine.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        public readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
    }
}