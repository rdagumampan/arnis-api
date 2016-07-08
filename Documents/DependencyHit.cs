using Newtonsoft.Json;

namespace Arnis.API.Models
{
    public class DependencyHit : DocumentBase
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("hits")]
        public int Hit { get; set; }
    }
}