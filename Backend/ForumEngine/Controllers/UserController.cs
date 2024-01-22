using AutoMapper;
using ForumEngine.Dtos;
using ForumEngine.Interfaces;
using ForumEngine.Models;
using Microsoft.AspNetCore.Mvc;

namespace ForumEngine.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        
        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = _mapper.Map<List<UserDto>>(await _userRepository.GetAllUsers());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUsersById(int id)
        {
            var user = _mapper.Map<UserDto>(await _userRepository.GetUserById(id));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserDto userCreate)
        {
            if (userCreate == null)
                return BadRequest(ModelState);

            var user = _mapper.Map<List<UserDto>>(await _userRepository.GetAllUsers()).Where(
                u => u.FirstName.Trim().ToUpper() == userCreate.FirstName.Trim().ToUpper() &&
                u.LastName.Trim().ToUpper() == userCreate.LastName.Trim().ToUpper()
            ).FirstOrDefault();

            if (user != null)
            {
                ModelState.AddModelError("", "User allready exists!");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userMap = _mapper.Map<User>(userCreate);
            await _userRepository.CreateUser(userMap);
            return Ok("Successfully created!");
        }
    }
}