using System.Collections.Generic;
using Newtonsoft.Json;

namespace Arnis.API.Models
{
    public class DependencyGroup: DocumentBase
    {
        [JsonProperty("targetFramework")]
        public string TargetFramework{ get; set; }

        [JsonProperty("dependencies")]
        public List<DependencyInfo> Dependencies { get; } = new List<DependencyInfo>();
    }
}