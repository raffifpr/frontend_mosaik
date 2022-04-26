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
        // accountstatus = ("child", "supervisor")
        public string accountStatus { get; set; }
        public SupervisorRequest[] supervisedRequests { get; set; }
        public SupervisedAccount[] supervisorAccounts { get; set; }
    }
}
