using System.Collections.Generic;
using Newtonsoft.Json;

namespace Arnis.Core.Documents
{
    public class DependencyGroup: DocumentBase
    {
        [JsonProperty("targetFramework")]
        public string TargetFramework{ get; set; }

        [JsonProperty("dependencies")]
        public List<DependencyInfo> Dependencies { get; } = new List<DependencyInfo>();
    }
}