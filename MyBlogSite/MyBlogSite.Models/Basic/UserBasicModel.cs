namespace MyBlogSite.Models.Basic
{
    /// <summary>
    /// 基本信息模型
    /// </summary>
    public class UserBasicModel
    {
        /// <summary>
        /// 用户基本信息Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 账户表Id
        /// </summary>
        public int AccountId { get; set; }

        /// <summary>
        /// 用户姓名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string Telephone { get; set; }

        /// <summary>
        /// 电子邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public bool Sex { get; set; }
    }
}