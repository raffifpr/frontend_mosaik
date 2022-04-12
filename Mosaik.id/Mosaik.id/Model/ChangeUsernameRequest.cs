using System;
using System.Collections.Generic;
using System.Text;

namespace Mosaik.id.Model
{
    public class ChangeUsernameRequest
    {
        public string Email { get; set; }
        public string NewUsername { get; set; }

    }
}
