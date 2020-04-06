using System;

namespace MyBlogSite.Models.Documents
{
    /// <summary>
    /// 文档模型
    /// </summary>
    public class DocumentsModel
    {
        /// <summary>
        /// 文章Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 文章标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime ModifyTime { get; set; }

        /// <summary>
        /// 文章内容
        /// </summary>
        public string ContentText { get; set; }

        /// <summary>
        /// 文章所属目录
        /// </summary>
        public int SubordinateCategory { get; set; }

        /// <summary>
        /// 点赞数
        /// </summary>
        public int LikenumInt { get; set; }

        /// <summary>
        /// 评论表Id(暂时不用)
        /// </summary>
        public string DiscussId { get; set; }

        /// <summary>
        /// 是否发布
        /// </summary>
        public bool IsPublish { get; set; }
    }
}