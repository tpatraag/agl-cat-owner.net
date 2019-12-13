using Newtonsoft.Json;

namespace AGL.CatOwner.Models
{
    public class Pet
    {
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
