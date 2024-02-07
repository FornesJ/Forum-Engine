using AutoMapper;
using ForumEngine.Dtos;
using ForumEngine.Interfaces;
using ForumEngine.Models;
using Microsoft.AspNetCore.Mvc;

namespace ForumEngine.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : Controller
    {
        private readonly IPostRepository _postRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;

        public PostController(IPostRepository postRepository, IUserRepository userRepository, ICommentRepository commentRepository, IMapper mapper)
        {
            _postRepository = postRepository;
            _userRepository = userRepository;
            _commentRepository = commentRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Post>))]
        public async Task<IActionResult> GetAllPosts()
        {
            var posts = _mapper.Map<List<PostDto>>(await _postRepository.GetAllPosts());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(posts);
        }

        [HttpGet("{postId}")]
        [ProducesResponseType(200, Type = typeof(Post))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetPostById(int postId)
        {
            var post = _mapper.Map<PostDto>(await _postRepository.GetPostById(postId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (post == null)
                return NotFound();

            return Ok(post);
        }

        [HttpGet("{postId}/Comments")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Comment>))]
        public async Task<IActionResult> GetCommentsToPost(int postId)
        {
            var comments = _mapper.Map<List<CommentDto>>(_postRepository.GetComments(postId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(comments);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreatePost([FromBody]PostDto postCreate, [FromQuery]int userId)
        {
            if (postCreate == null)
                return BadRequest(ModelState);

            User user = await _userRepository.GetUserById(userId);

            if (user == null)
            {
                return NotFound("User does not exist!");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var postMap = _mapper.Map<Post>(postCreate);

            if (!_postRepository.CreatePost(postMap, user))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully created!");
        }

        [HttpPut("{postId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdatePost(int postId, [FromBody]PostDto updatedPost)
        {
            if (updatedPost == null)
                return BadRequest(ModelState);

            if (postId != updatedPost.Id)
                return BadRequest(ModelState);

            if (!_postRepository.PostExists(postId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var postMap = _mapper.Map<Post>(updatedPost);

            if (!_postRepository.UpdatePost(postMap))
            {
                ModelState.AddModelError("", "Something wnet wrong while updating post!");
                return StatusCode(500, ModelState); 
            }

            return NoContent();
        }

        [HttpDelete("{postId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeletePost(int postId)
        {
            if (!_postRepository.PostExists(postId))
                return NotFound();

            var postToDelete = await _postRepository.GetPostById(postId);
            var commentsToDelete = _postRepository.GetComments(postId);

            if (!ModelState.IsValid)
                return BadRequest();

            if (!_commentRepository.DeleteComments(commentsToDelete.ToList()))
            {
                ModelState.AddModelError("", "Something went wrong when deleting comments!");
            }

            if (!_postRepository.DeletePost(postToDelete))
            {
                ModelState.AddModelError("", "Something went wrong when deleting post!");
            }

            return NoContent();
        }
    }
}