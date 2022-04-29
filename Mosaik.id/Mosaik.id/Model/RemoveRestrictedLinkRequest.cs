using System;
using System.Collections.Generic;
using System.Text;

namespace Mosaik.id.Model
{
    public class RemoveRestrictedLinkRequest
    {
        public string email { get; set; }
        public string link { get; set; }
    }
}
