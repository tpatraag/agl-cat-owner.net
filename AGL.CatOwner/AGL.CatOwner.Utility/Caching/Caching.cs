using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text.RegularExpressions;

namespace AGL.CatOwner.Utility
{
    public class Caching:ICaching
    {
        private string sessionId = "MyRandomSessionIdjshfbgwo87t34iot7894y";
        private const string regionName = "Mem_Cache";
        private ObjectCache cache = MemoryCache.Default;
        public const double DefaultCacheDuration = 20;
        #region Private Functions
        private string concatArgs(List<string> args)
        {
            string result = string.Empty;
            foreach (string str in args)
            {
                Regex rgx = new Regex("[^a-zA-Z0-9 -]");
                string st = string.IsNullOrWhiteSpace(str) ? "" : str;
                result += "_" + rgx.Replace(st, "_");
            }
            return result;
        }

        private string getKeyName(string[] args, params string[] prms)
        {
            return concatArgs(prms.ToList().Concat(args).ToList());
        }

        private string getKey(bool isSessionSpecific, string[] args, string key)
        {
            return isSessionSpecific ? getKeyName(args, key, sessionId) : getKeyName(args, key);
        }
        #endregion

        #region Cache Internal Functions
        public Object Get(string key, bool isSessionSpecific, params string[] args)
        {
            string keyName = getKey(isSessionSpecific, args, key);
            if (this.cacheExists(keyName))
            {
                return cache.Get(keyName);
            }
            else
            {
                return null;
            }
        }

        public bool Set(string key, object value, bool isSessionSpecific, double? duration, params string[] args)
        {
            string keyName = getKey(isSessionSpecific, args, key);
            try
            {
                CacheItemPolicy policy = new CacheItemPolicy();
                //policy.SlidingExpiration = new TimeSpan(0, 30, 0);
                if (isSessionSpecific)
                {
                    policy.SlidingExpiration = new TimeSpan(0, 0, (int)(duration.HasValue ? duration : DefaultCacheDuration), 0);
                }
                else
                {
                    policy.AbsoluteExpiration = DateTimeOffset.Now.AddMinutes((double)(duration.HasValue ? duration : DefaultCacheDuration));
                }
                policy.Priority = CacheItemPriority.Default;

                CacheItem item = new CacheItem(keyName, value, regionName);
                cache.Set(item, policy);
                return true;
            }
            catch //(Exception ex)
            {
                //WaoLogger.HandleException(new Exception("Error adding cache key: " + keyName, ex));
                return false;
            }
        }

        public bool Remove(string key, bool isSessionSpecific = true, params string[] args)
        {
            string keyName = getKey(isSessionSpecific, args, key);
            try
            {
                if (this.cacheExists(keyName))
                {
                    cache.Remove(keyName);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch //(Exception ex)
            {
                //WaoLogger.HandleException(new Exception("Error removing cache key: " + keyName, ex));
                return false;
            }
        }

        private bool cacheExists(string keyName)
        {
            return cache.Contains(keyName);
        }

        public bool Exists(string key, bool isSessionSpecific, params string[] args)
        {
            //string keyName = getKey(isSessionSpecific, args, key);
            return cache.Contains(getKey(isSessionSpecific, args, key));
        }
        #endregion
    }
}
