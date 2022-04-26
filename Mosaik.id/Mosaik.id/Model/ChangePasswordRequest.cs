using System;
using System.Collections.Generic;
using System.Text;

namespace Mosaik.id.Model
{
    public class ChangePasswordRequest
    {
        public string email { get; set; }
        public string oldPassword { get; set; }
        public string newPassword { get; set; }
    }
}
