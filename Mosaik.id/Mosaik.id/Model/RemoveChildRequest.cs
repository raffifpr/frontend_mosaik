using System;
using System.Collections.Generic;
using System.Text;

namespace Mosaik.id.Model
{
    public class RemoveChildRequest
    {
        // Parent Email
        public string email { get; set; }
        public string childEmail { get; set; }

    }
}
