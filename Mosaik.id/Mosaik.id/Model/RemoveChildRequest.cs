using System;
using System.Collections.Generic;
using System.Text;

namespace Mosaik.id.Model
{
    public class RemoveChildRequest
    {
        // Parent Email
        public string Email { get; set; }
        public string ChildEmail { get; set; }

    }
}
