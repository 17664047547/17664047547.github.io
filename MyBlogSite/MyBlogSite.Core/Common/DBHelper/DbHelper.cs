using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyBlogSite.Models.DTOs;

namespace MyBlogSite.Core.Common.DBHelper
{
    public class DapperHelper
    {
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        private static readonly string ConnectionString =
            ConfigurationManager.ConnectionStrings["db"].ConnectionString;

        /// <summary>
        /// dapper通用分页方法
        /// </summary>
        /// <typeparam name="T">泛型集合实体类</typeparam>
        /// <param name="tableName">表</param>
        /// <param name="where">条件</param>
        /// <param name="orderby">排序</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">当前页显示条数</param>
        /// <returns></returns>
        public async Task<PageResult<T>> GetPageList<T>(string tableName, string where,
            string orderby, int pageIndex, int pageSize)
        {
            using (var conn = new SqlConnection(ConnectionString))
            {
                var sql = $"select top {pageSize} * from (select row_number() over(order by {orderby}) as rownumber,* from {tableName}) temp_row where rownumber>(({pageIndex}-1)*{pageSize})";
                var list = await conn.QueryAsync<T>(sql);
            }
        }
    }
}