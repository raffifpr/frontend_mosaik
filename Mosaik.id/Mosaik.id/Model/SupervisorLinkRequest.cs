using System;
using System.Collections.Generic;
using System.Text;

namespace Mosaik.id.Model
{
    public class SupervisorLinkRequest
    {
        public string email { get; set; }
        public string emailSupervisor { get; set; }
        // statusAccept = ("accept", "deny")
        public string statusAccept { get; set; }
    }
}
