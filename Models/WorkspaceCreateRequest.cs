using System.Collections.Generic;
using Arnis.API.Models;

namespace Arnis.API.Models
{
    public class WorkspaceCreateRequest
    {
        public string ApiKey { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public List<string> Owners { get; set; }

        public List<Solution> Solutions { get; set; } = new List<Solution>();
        public List<string> Logs { get; set; } = new List<string>();
    }
}
