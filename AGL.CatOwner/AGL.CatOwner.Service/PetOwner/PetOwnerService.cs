using AGL.CatOwner.Models;
using AGL.CatOwner.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace AGL.CatOwner.Service.PetOwner
{
    public class PetOwnerService : IPetOwnerService
    {
        private ICaching cache;

        public PetOwnerService(ICaching cache)
        {
            this.cache = cache;
        }

        public IEnumerable<PetOwnerPerson> GetAllPetOwner()
        {
            try
            {
                string _petOwnerResultStream;
                bool _isProxyEnabled = Convert.ToBoolean(ConfigurationManager.AppSettings[Constants.UseProxy]);
                ProxyDetail _proxyDetail = null;
                if (_isProxyEnabled)
                {
                    _proxyDetail = new ProxyDetail
                    {
                        Url = ConfigurationManager.AppSettings[Constants.ProxyUrl].ToString(),
                        Port = ConfigurationManager.AppSettings[Constants.ProxyPort].ToString()
                    };

                }

                Dictionary<string, string> _headerDetails = new Dictionary<string, string>();
                _headerDetails.Add("User-Agent", ConfigurationManager.AppSettings[Constants.ExtApiUserAgent].ToString());

                bool _isMemoryCacheEnabled = Convert.ToBoolean(ConfigurationManager.AppSettings[Constants.MemCacheAppSettings]);

                //Check in Cache
                if (_isMemoryCacheEnabled)
                {
                    if (null == this.cache.Get(Constants.PetOwnerCache, false))
                    {
                        _petOwnerResultStream = APIHandler.GetAPIResult(ConfigurationManager.AppSettings[Constants.ApiUrl], _headerDetails, _isProxyEnabled, _proxyDetail); //.Result; // httpClient.GetStringAsync(new Uri(ConfigurationManager.AppSettings[Constants.ApiUrl])).Result;
                        cache.Set(Constants.PetOwnerCache, _petOwnerResultStream, false, null);
                    }
                    else
                    {
                        _petOwnerResultStream = Convert.ToString(cache.Get(Constants.PetOwnerCache, false));
                    }
                }
                else
                {
                    _petOwnerResultStream = APIHandler.GetAPIResult(ConfigurationManager.AppSettings[Constants.ApiUrl], _headerDetails, _isProxyEnabled, _proxyDetail);
                }


                JsonSerializerSettings settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore
                };
                return JsonConvert.DeserializeObject<List<PetOwnerPerson>>(_petOwnerResultStream, settings);
            }
            catch (Exception ex)
            {
                Logging.HandleException(ex);
                throw;
            }
        }
    }
}
