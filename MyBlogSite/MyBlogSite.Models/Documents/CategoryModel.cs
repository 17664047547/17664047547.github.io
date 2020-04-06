namespace MyBlogSite.Models.Documents
{
    /// <summary>
    /// 目录模型
    /// </summary>
    public class CategoryModel
    {
        /// <summary>
        /// 目录唯一Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 目录名称
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// 目录等级 0:未分类,1:一级目录,2:二级目录,3:三级目录
        /// </summary>
        public int CategoryLevel { get; set; }

        /// <summary>
        /// 附属目录Id拼接而成的字符串
        /// </summary>
        public string AffiliateCategory { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
    }
}