using Microsoft.EntityFrameworkCore;
using TheBlogProject.Data;
using TheBlogProject.Models;
using TheBlogProject.Services.Interfaces;

namespace TheBlogProject.Services
{
    public class PostService : IPostService
    {

        private readonly ApplicationDbContext _context;

        public PostService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddNewPostAsync(Post post)
        {
            _context.Add(post);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Post>> GetAllPostAsync()
        {
            List<Post> post = await _context.Posts
                .Include(p => p.Tags)
                .Include(p => p.Category)
                .Include(p => p.PostUser)
                .Include(p => p.Comments)
                .ToListAsync();
            return post;
        }

        public IQueryable<Post> GetLatestPostsAsync()
        {
            // This method now returns an IQueryable<Post>
            return _context.Posts
                .Include(p => p.Tags)
                .Include(p => p.Category)
                .Include(p => p.PostUser)
                .Include(p => p.Comments)
                .OrderByDescending(p => p.Created);
        }

        public async Task<List<Post>> GetAllPostAsync(int count)
        {
            List<Post> post = await _context.Posts.Take(count)
                .Include(p => p.Tags)
                .Include(p => p.Category)
                .Include(p => p.PostUser)
                .Include(p => p.Comments)
                .ToListAsync();
            return post;
        }

        public async Task<Post> GetPostByIdAsync(int PostId)
        {
            Post post = await _context.Posts

                .Include(p => p.Tags)
                .Include(p => p.Category)
                .Include(p => p.PostUser)
                .Include(p => p.Comments)
                .ThenInclude( c=> c.BlogUser )

                .FirstOrDefaultAsync(p => p.Id == PostId);

            return post;
        }

        public async Task RemovePostAsync(Post post)
        {
            _context.Posts.Remove(post);
        }

        public async Task UpdatePostAsync(Post post)
        {
            _context.Update(post);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Post>> GetMostPopularPostsBasedOnComments()
        {
            var posts = await _context.Posts.OrderByDescending(p => p.Comments.Count).ToListAsync();
            return posts;
        }
    }
}
