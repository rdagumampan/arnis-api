using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Arnis.API.Models
{
    public class Workspace: DocumentBase
    {
        [JsonProperty("id")]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [JsonProperty("accountId")]
        public string AccountId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("owners")]
        public List<string> Owners { get; set; }

        [JsonProperty("solutions")]
        public List<Solution> Solutions { get; set; } = new List<Solution>();
    }
}