using AGL.CatOwner.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace AGL.CatOwner.Utility
{
    public class APIHandler
    {
        public static string GetAPIResult(string apiUri, Dictionary<string, string> headerDetails = null, bool useProxy = false, ProxyDetail proxyDetail = null)
        {
            HttpClientHandler _httpClientHandler = new HttpClientHandler();
            if (useProxy && null != proxyDetail)
            {
                _httpClientHandler.Proxy = new WebProxy(string.Format("{0}:{1}", proxyDetail.Url, proxyDetail.Port), false);
                _httpClientHandler.UseProxy = true;
            }
            using (HttpClient httpClient = new HttpClient(_httpClientHandler))
            {
                httpClient.DefaultRequestHeaders.Clear();
                if (null != headerDetails)
                    foreach (KeyValuePair<string, string> header in headerDetails)
                    {
                        httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
                    }
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                return httpClient.GetStringAsync(new Uri(apiUri)).Result;
            }
        }
    }
}
