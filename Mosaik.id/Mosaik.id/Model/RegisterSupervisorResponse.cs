using System;
using System.Collections.Generic;
using System.Text;

namespace Mosaik.id.Model
{
    public class RegisterSupervisorResponse
    {
        // status = ("success", "failed", "email already exist", "index 1 child email doesn't exist")
        public string status { get; set; }
    }
}
