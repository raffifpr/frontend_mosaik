using System;
using System.Collections.Generic;
using System.Text;

namespace Mosaik.id.Model
{
    public class ChangePasswordRequest
    {
        public string Email { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
