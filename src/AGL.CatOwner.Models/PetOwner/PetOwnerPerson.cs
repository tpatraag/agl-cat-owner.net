using Newtonsoft.Json;
using System.Collections.Generic;

namespace AGL.CatOwner.Models
{
    public class PetOwnerPerson
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("gender")]
        public string Gender { get; set; }
        [JsonProperty("age")]
        public int Age { get; set; }
        [JsonProperty("pets")]
        public List<Pet> Pets { get; set; }
    }
}
