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
        private readonly IPostRepository _postRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;
        
        public UserController(IUserRepository userRepository, IPostRepository postRepository, ICommentRepository commentRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _postRepository = postRepository;
            _commentRepository = commentRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<User>))]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = _mapper.Map<List<UserDto>>(await _userRepository.GetAllUsers());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(users);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetUsersById(int id)
        {
            var user = _mapper.Map<UserDto>(await _userRepository.GetUserById(id));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        /*
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Post>))]
        public async Task<IActionResult> GetAllUserPosts(int id)
        {
            var posts = _mapper.Map<List<PostDto>>(await _userRepository.GetUserPosts(id));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(posts);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Comment>))]
        public async Task<IActionResult> GetAllUserComments(int id)
        {
            var comments = _mapper.Map<List<CommentDto>>(await _userRepository.GetUserComments(id));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(comments);
        }
        */
        
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
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
            
            if (!_userRepository.CreateUser(userMap))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created!");
        }
    }
}