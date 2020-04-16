using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using DapperExtensions;
using MyBlogSite.Models.DTOs;

namespace MyBlogSite.Core.Common.DapperHelper
{
    public static class DapperHelper
    {
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        private static readonly string ConnectionString =
            ConfigurationManager.ConnectionStrings["db"].ConnectionString;

        private static SqlConnection GetConnection()
        {
            var connection = new SqlConnection(ConnectionString);
            connection.Open();
            return connection;
        }

        public static T Get<T>(int id) where T : class
        {
            using var conn = GetConnection();
            var ob = conn.Get<T>(id);
            return ob;
        }


        #region 查询方法

        public static async Task<IEnumerable<T>> SqlQueryAsync<T>(string sql, object param = null)
        {
            using var conn = GetConnection();
            var list = await conn.QueryAsync<T>(sql, param, null, conn.ConnectionTimeout, null);
            return list;
        }

        public static IEnumerable<T> SqlQuery<T>(string sql, object param = null)
        {
            using var conn = GetConnection();
            var list = conn.Query<T>(sql, param, null, true, conn.ConnectionTimeout, null);
            return list;
        }

        public static IEnumerable<dynamic> SqlQuery(string sql, object param = null)
        {
            using var conn = GetConnection();
            var list = conn.Query(sql, param, null, true, conn.ConnectionTimeout, null);
            return list;
        }

        public static SqlMapper.GridReader SqlQueryMultiple(string sql, object param = null)
        {
            using var conn = GetConnection();
            var list = conn.QueryMultiple(sql, param);
            return list;
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="conn"></param>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="orders"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static async Task<PageResult<T>> QueryPageAsync<T>(this IDbConnection conn,
            string sql,
            object param,
            string orders,
            int page,
            int pageSize) where T : class
        {
            var pagedSql = "";
            try
            {
                var startIndex = (page - 1) * pageSize + 1;
                var endIndex = page * pageSize;
                pagedSql =
                    $@"
with queryData as ({sql})
SELECT *
FROM (SELECT ROW_NUMBER() OVER ( ORDER BY {orders}) AS RowNum, * FROM queryData) AS result
WHERE RowNum >= {startIndex} AND RowNum <= {endIndex}
ORDER BY RowNum";

                var countSql = $"with queryData as ({sql})  select count(1) from queryData";
                var reader = await conn.QueryMultipleAsync($"{countSql};{pagedSql}", param,
                    commandTimeout: conn.ConnectionTimeout);

                var count = (await reader.ReadAsync<int>()).First();
                var list = await reader.ReadAsync<T>();

                return list.ToPageResult(page, pageSize, count);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion

        #region 执行方法

        #region 同步

        public static long Insert<T>(T entityToInsert) where T : class
        {
            using var conn = GetConnection();
            var result = conn.Insert(entityToInsert);
            return result;
        }

        public static bool Update<T>(T entityToInsert) where T : class
        {
            using var conn = GetConnection();
            var result = conn.Update(entityToInsert);
            return result;
        }

        public static int SqlExecute(string sql, object param = null, IDbTransaction transactionn = null)
        {
            using var conn = GetConnection();
            var result = conn.Execute(sql, param, transactionn);
            return result;
        }


        /// <summary>
        /// 运用事务处理系列sql,有一句失败则全部回滚
        /// </summary>
        /// <param name="sqls"></param>
        /// <returns></returns>
        public static int[] SqlExecuteWithTransaction(List<Tuple<string, object>> sqls)
        {
            if (sqls.Count == 0)
            {
                return null;
            }

            using (var conn = GetConnection())
            {
                var trans = conn.BeginTransaction();
                try
                {
                    var firstSql = sqls[0].Item1;
                    var firstParam = sqls[0].Item2;
                    var result = new int[sqls.Count];
                    result[0] = conn.Execute(firstSql, firstParam, trans);
                    for (int i = 1; i < sqls.Count; i++)
                    {
                        result[i] = conn.Execute(sqls[i].Item1, sqls[i].Item2, trans);
                    }

                    trans.Commit();
                    return result;
                }
                catch (Exception)
                {
                    trans.Rollback();
                }
            }

            return null;
        }


        public static bool Update<T>(T entityToUpdate, IDbTransaction transaction = null, int? commandTimeout = null)
            where T : class
        {
            using var conn = DapperHelper.GetConnection();
            var result = conn.Update(entityToUpdate, transaction, commandTimeout);
            return result;
        }


        public static int Inert<T>(T entityToUpdate, IDbTransaction transaction = null, int? commandTimeout = null)
            where T : class
        {
            using var conn = GetConnection();
            var result = conn.Insert(entityToUpdate, transaction, commandTimeout);
            return (int) result;
        }

        #endregion

        #region 异步

        public static async Task<int> SqlExecuteAsync(string sql, object param = null)
        {
            using var conn = GetConnection();
            var result = await conn.ExecuteAsync(sql, param);
            return result;
        }


        public static async Task<int> InsertAsync<T>(T entityToUpdate, IDbTransaction transaction = null,
            int? commandTimeout = null)
            where T : class
        {
            using var conn = GetConnection();
            var result = await conn.InsertAsync(entityToUpdate, transaction, commandTimeout);
            return (int) result;
        }

        public static int Insert<T>(T entityToUpdate, IDbTransaction transaction = null, int? commandTimeout = null)
            where T : class
        {
            using var conn = GetConnection();
            var result = conn.Insert<T>(entityToUpdate, transaction, commandTimeout);
            return (int) result;
        }

        public static async Task<bool> UpdateAsync<T>(T entityToUpdate, IDbTransaction transaction = null,
            int? commandTimeout = null)
            where T : class
        {
            using var conn = GetConnection();
            var result = await conn.UpdateAsync(entityToUpdate, transaction, commandTimeout);
            return result;
        }

        /// <summary>
        /// 运用事务处理系列sql,有一句失败则全部回滚
        /// </summary>
        /// <param name="sqls"></param>
        /// <returns></returns>
        public static async Task<int[]> SqlExecuteAsyncWithTransaction(List<Tuple<string, object>> sqls)
        {
            if (sqls.Count == 0) return null;
            using var conn = GetConnection();
            var trans = conn.BeginTransaction();
            var firstSql = sqls[0].Item1;
            var firstParam = sqls[0].Item2;
            var result = new int[sqls.Count];
            result[0] = await conn.ExecuteAsync(firstSql, firstParam, trans);
            for (var i = 1; i < sqls.Count; i++)
            {
                result[i] = await conn.ExecuteAsync(sqls[i].Item1, sqls[i].Item2, trans);
            }

            if (result.AsList().Contains(0))
                trans.Rollback();
            else
                trans.Commit();
            return result;
        }

        #endregion

        #endregion


        #region Extensions

        private static PageResult<T> ToPageResult<T>(this IEnumerable<T> items,
            int pageIndex,
            int pageSize,
            int rowCount
        )
        {
            if (pageIndex < 1)
                pageIndex = 1;
            var totalItemCount = rowCount;
            return new PageResult<T>(items.ToArray(), pageIndex, pageSize, totalItemCount);
        }

        #endregion
    }
}
