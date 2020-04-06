using System.Threading.Tasks;
using MyBlogSite.Models.Account;

namespace MyBlogSite.Repo
{
    public interface IAccountRepo
    {
        /// <summary>
        /// 根据账户Id获取账户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<AccountModel> GetAccountInfoById(int id);
        
        /// <summary>
        /// 根据账户密码获取用户信息
        /// </summary>
        /// <param name="account"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<AccountModel> GetAccountInfoByPassword(string account,string password);

        /// <summary>
        /// 根据账户Id修改用户信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<bool> UpdateAccountInfoByModelId(AccountModel model);

    }
}