
using System;
using Newtonsoft.Json;

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

        [JsonProperty("dateCreated")]
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;

        [JsonProperty("dateUpdated")]
        public DateTime DateUpdated { get; set; } = DateTime.UtcNow;
    }
}