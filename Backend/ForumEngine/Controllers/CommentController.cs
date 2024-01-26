using AutoMapper;
using ForumEngine.Dtos;
using ForumEngine.Interfaces;
using ForumEngine.Models;
using Microsoft.AspNetCore.Mvc;

namespace ForumEngine.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : Controller
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IPostRepository _postRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public CommentController(ICommentRepository commentRepository, 
        IPostRepository postRepository, 
        IUserRepository userRepository, 
        IMapper mapper)
        {
            _commentRepository = commentRepository;
            _postRepository = postRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Comment>))]
        public async Task<IActionResult> GetAllComments()
        {
            var comments = _mapper.Map<List<CommentDto>>(await _commentRepository.GetAllComments());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(comments);
        }

        [HttpGet("{commentId}")]
        [ProducesResponseType(200, Type = typeof(Comment))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetCommentById(int commentId)
        {
            var comment = _mapper.Map<CommentDto>(await _commentRepository.GetCommentById(commentId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (comment == null)
                return NotFound();

            return Ok(comment);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateComment([FromBody]CommentDto commentCreate, [FromQuery]int postId, [FromQuery]int userId)
        {
            if (commentCreate == null)
                return BadRequest(ModelState);

            Post post = await _postRepository.GetPostById(postId);

            if (post == null)
                return NotFound("Post does not exist!");

            User user = await _userRepository.GetUserById(userId);

            if (user == null)
                return NotFound("User does not exist!");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var commentMap = _mapper.Map<Comment>(commentCreate);
            if (!_commentRepository.CreateComment(commentMap, post, user))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully created!");
        }
    }
}