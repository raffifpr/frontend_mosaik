using System;
using System.Collections.Generic;
using System.Text;

namespace Mosaik.id.Model
{
    public class LoginResponse
    {
        // Status = ("success", "failed", "wrong")
        public string status { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        // accountStatus = ("child", "supervisor")
        public string accountStatus { get; set; }
        public SupervisorRequest[] supervisorRequests { get; set; }
        public SupervisedAccount[] supervisedAccounts { get; set; }
    }
}
