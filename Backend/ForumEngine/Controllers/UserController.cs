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

        [HttpGet("{userId}/Posts")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Post>))]
        public async Task<IActionResult> GetPostsByUser(int userId)
        {
            var posts = _mapper.Map<List<PostDto>>(await _userRepository.GetUserPosts(userId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(posts);
        }

        [HttpGet("{userId}/Comments")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Comment>))]
        public async Task<IActionResult> GetCommentsByUser(int userId)
        {
            var comments = _mapper.Map<List<CommentDto>>(await _userRepository.GetUserComments(userId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(comments);
        }
        
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
            
            if (await _userRepository.CreateUser(userMap) == null)
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created!");
        }

        [HttpPut("{userId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateUser(int userId, [FromBody]UserDto updatedUser)
        {
            if (updatedUser == null)
                return BadRequest(ModelState);

            if (!_userRepository.UserExists(userId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var userMap = _mapper.Map<User>(updatedUser);

            if (await _userRepository.UpdateUser(userId, userMap) == null)
            {
                ModelState.AddModelError("", "Something wnet wrong while updating user");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{userId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            if (!_userRepository.UserExists(userId))
                return NotFound();

            var userToDelete = await _userRepository.GetUserById(userId);
            var postsToDelete = await _userRepository.GetUserPosts(userId);
            var commentsToDelete = await _userRepository.GetUserComments(userId);

            if (!ModelState.IsValid)
                return BadRequest();

            foreach (Post post in postsToDelete)
            {
                var commentsInPostToDelete = await _postRepository.GetComments(post.Id);

                if (!ModelState.IsValid)
                    return BadRequest();
                
                if (!_commentRepository.DeleteComments(commentsInPostToDelete))
                {
                    ModelState.AddModelError("", "Something went wrong when deleting comments in post!");
                }
            }

            if (!_commentRepository.DeleteComments(commentsToDelete.ToList()))
            {
                ModelState.AddModelError("", "Something went wrong when deleting comments!");
            }

            if (!_postRepository.DeletePosts(postsToDelete.ToList()))
            {
                ModelState.AddModelError("", "Something went wrong when deleting posts!");
            }

            if (await _userRepository.DeleteUser(userToDelete) == null)
            {
                ModelState.AddModelError("", "Something went wrong when deleting user!");
            }

            return NoContent();
        }
    }
}