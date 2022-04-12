using System;
using System.Collections.Generic;
using System.Text;

namespace Mosaik.id.Model
{
    public class LoginResponse
    {
        public string username { get; set; }
        public string email { get; set; }
        public string accountStatus { get; set; }
        public SupervisorRequest[] supervisorRequests { get; set; }
        public SupervisedAccount[] supervisedAccounts { get; set; }
    }
}
