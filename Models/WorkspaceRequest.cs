﻿using System.Collections.Generic;
using Arnis.API.Models;

namespace Arnis.API.Models
{
    public class WorkspaceRequest
    {
        public string ApiKey { get; set; }
        public List<Solution> Solutions { get; set; } = new List<Solution>();
    }
}