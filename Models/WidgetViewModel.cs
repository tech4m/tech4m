using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using tech4mEntity;

namespace tech4mUI.Models
{
    /// <summary>
    /// View model used to wrap data for sidebar widgets.
    /// </summary>
    public class WidgetViewModel
    {
        public WidgetViewModel(Tech4mService.Tech4mServiceClient blogRepository)
        {
            Categories = blogRepository.Categories();
            Tags = blogRepository.Tags();
            //LatestPosts = blogRepository.Posts(0, 10);
            LatestPosts = blogRepository.PostWithPaging(0, 10);
        }

        public IList<Category> Categories
        { get; private set; }

        public IList<Tag> Tags
        { get; private set; }

        public IList<Post> LatestPosts
        { get; private set; }
    }
}