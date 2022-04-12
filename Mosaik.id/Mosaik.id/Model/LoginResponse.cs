using System;
using System.Collections.Generic;
using System.Text;

namespace Mosaik.id.Model
{
    public class LoginResponse
    {
        public string accountStatus { get; set; }
        public SupervisorRequest[] supervisorRequests { get; set; }
        public SupervisedAccount[] supervisedAccounts { get; set; }
    }
}
