using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using tech4mEntity;

namespace tech4mUI.Models
{
    /// <summary>
    /// View model used for list view.
    /// </summary>
    /// <remarks>
    /// Same view model is used to list posts for category, tag or search text.
    /// </remarks>
    public class ListViewModel
    {
        public ListViewModel(Tech4mService.Tech4mServiceClient blogRepository, int p)
        {
            //Posts = blogRepository.Posts(p - 1, 10);
            Posts = blogRepository.PostWithPaging(p - 1, 10);
            TotalPosts = blogRepository.TotalPosts(true);
        }

        public ListViewModel(Tech4mService.Tech4mServiceClient blogRepository, string text, string type, int p)
        {
            switch (type)
            {
                case "Category":
                    Posts = blogRepository.PostsForCategory(text, p - 1, 10);
                    TotalPosts = blogRepository.TotalPostsForCategory(text);
                    Category = blogRepository.Category(text);
                    break;

                case "Tag":
                    Posts = blogRepository.PostsForTag(text, p - 1, 10);
                    TotalPosts = blogRepository.TotalPostsForTag(text);
                    Tag = blogRepository.Tag(text);
                    break;

                default:
                    Posts = blogRepository.PostsForSearch(text, p - 1, 10);
                    TotalPosts = blogRepository.TotalPostsForSearch(text);
                    Search = text;
                    break;
            }
        }

        public IList<Post> Posts
        { get; private set; }

        public int TotalPosts
        { get; private set; }

        public Category Category
        { get; private set; }

        public Tag Tag
        { get; private set; }

        public string Search
        { get; private set; }
    }
}