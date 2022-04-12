using System;
using System.Collections.Generic;
using System.Text;

namespace Mosaik.id.Model
{
    public class RegisterSupervisorRequest
    {
        public string username { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string[] supervisedEmail { get; set; }
    }
}
