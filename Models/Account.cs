
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Arnis.API.Models
{
    public class Account : DocumentBase
    {
        [JsonProperty("id")]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [JsonProperty("apiKey")]
        public string ApiKey { get; set; }

        [JsonProperty("userName")]
        public string UserName { get; set; }
    }
}