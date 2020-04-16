using System;
using System.Collections.Generic;
using System.Linq;

namespace MyBlogSite.Models.DTOs
{
    public class PageResult<T>
    {
        public PageResult()
        {
        }

        public PageResult(IEnumerable<T> list, int page, int pageSize, int totalCount)
        {
            Results = list.ToArray();
            CurrentPage = page;
            PageSize = pageSize;
            RowCount = totalCount;
        }

        public T[] Results { get; set; }

        /// <summary>
        /// 数据汇总信息
        /// </summary>
        public int CurrentPage { get; set; }

        public int PageSize { get; set; }
        public int RowCount { get; set; }

        public int PageCount
        {
            get
            {
                var pageCount = (int) Math.Ceiling((double) RowCount / PageSize);
                if (pageCount > 0 && pageCount <= 100)
                {
                    return pageCount;
                }

                return pageCount > 100 ? 100 : 1;
            }
        }

        public static PageResult<T> EmptyPage(int page, int pageSize, int totalCount)
        {
            return new PageResult<T>(Enumerable.Empty<T>().ToArray(), page, pageSize, totalCount);
        }
    }
}