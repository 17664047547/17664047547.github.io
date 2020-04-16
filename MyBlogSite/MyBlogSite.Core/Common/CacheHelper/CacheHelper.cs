using System;
using System.Collections;
using System.Web;
using System.Web.Caching;

namespace MyBlogSite.Core.Common.CacheHelper
{
    /// <summary>
    /// 一般缓存类
    /// </summary>
    public class CacheHelper
    {
       /// <summary>
       /// 获取当前应用程序指定CacheKey的Cache值
       /// </summary>
       /// <param name="cacheKey"></param>
       /// <returns></returns>
        public static object GetCache(string cacheKey)
        {
            var objCache = HttpRuntime.Cache;
            return objCache[cacheKey];
        }

        /// <summary>
        /// 设置当前应用程序指定CacheKey的Cache值
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <param name="objObject"></param>
        public static void SetCache(string cacheKey, object objObject)
        {
            var objCache = HttpRuntime.Cache;
            objCache.Insert(cacheKey, objObject);
        }


        /// <summary>
        /// 设置当前应用程序指定CacheKey的Cache值
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <param name="objObject"></param>
        /// <param name="absoluteExpiration"></param>
        /// <param name="slidingExpiration"></param>
        public static void SetCache(string cacheKey, object objObject, DateTime absoluteExpiration,
            TimeSpan slidingExpiration)
        {
            var objCache = HttpRuntime.Cache;
            objCache.Insert(cacheKey, objObject, null, absoluteExpiration, slidingExpiration);
        }

       /// <summary>
       /// 设置数据缓存
       /// </summary>
       /// <param name="cacheKey"></param>
       /// <param name="objObject"></param>
       /// <param name="timeout"></param>
        public static void SetCache(string cacheKey, object objObject, int timeout = 7200)
        {
            try
            {
                if (objObject == null) return;
                var objCache = HttpRuntime.Cache;
                //相对过期  
                //objCache.Insert(cacheKey, objObject, null, DateTime.MaxValue, timeout, CacheItemPriority.NotRemovable, null);  
                //绝对过期时间  
                objCache.Insert(cacheKey, objObject, null, DateTime.Now.AddSeconds(timeout), TimeSpan.Zero,
                    CacheItemPriority.High, null);
            }
            catch
            {
                // ignored
            }
        }

       /// <summary>
       /// 清除单一键缓存
       /// </summary>
       /// <param name="cacheKey"></param>
        public static void RemoveKeyCache(string cacheKey)
        {
            try
            {
                var objCache = HttpRuntime.Cache;
                objCache.Remove(cacheKey);
            }
            catch
            {
                // ignored
            }
        }

        /// <summary>
        /// 清除所有缓存
        /// </summary>
        public static void RemoveAllCache()
        {
            var cache = HttpRuntime.Cache;
            var cacheEnum = cache.GetEnumerator();
            if (cache.Count <= 0) return;
            var al = new ArrayList();
            while (cacheEnum.MoveNext())
            {
                al.Add(cacheEnum.Key);
            }

            foreach (string key in al)
            {
                cache.Remove(key);
            }
        }

        // /// <summary>  
        // /// 清除所有缓存
        // /// </summary>  
        // public static void RemoveAllCache()
        // {
        //     var cache = HttpRuntime.Cache;
        //     var cacheEnum = cache.GetEnumerator();
        //     while (cacheEnum.MoveNext())
        //     {
        //         cache.Remove(cacheEnum.Key.ToString());
        //     }
        // }

        /// <summary>
        /// 以列表形式返回已存在的所有缓存 
        /// </summary>
        /// <returns></returns> 
        public static ArrayList ShowAllCache()
        {
            var al = new ArrayList();
            var cache = HttpRuntime.Cache;
            if (cache.Count <= 0) return al;
            var cacheEnum = cache.GetEnumerator();
            while (cacheEnum.MoveNext())
            {
                al.Add(cacheEnum.Key);
            }

            return al;
        }
    }
}