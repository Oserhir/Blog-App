using TheBlogProject.Models;

namespace TheBlogProject.ViewModels
{
    public class HomePageViewModel
    {
        public List<Post>? Posts { get; set; }
        public List<Post>? LatestPosts { get; set; }
        public List<Category>? Categories { get; set; }
        public List<Post>? PopularPosts { get; set; }

    }
}
