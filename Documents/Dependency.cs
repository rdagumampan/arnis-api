using Newtonsoft.Json;

namespace Arnis.Core.Documents
{
    public class Dependency: DocumentBase
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }
    }
}