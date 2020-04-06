using System.Collections.Generic;

namespace MyBlogSite.Models.DTOs
{
    public class PageResult<T>
    {
        public IEnumerable<T> List { get; set; }
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
    }
}