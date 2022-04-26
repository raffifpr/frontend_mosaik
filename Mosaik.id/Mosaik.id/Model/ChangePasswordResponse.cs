using System;
using System.Collections.Generic;
using System.Text;

namespace Mosaik.id.Model
{
    public class ChangePasswordResponse
    {
        // status = "wrong password", "success", "failed"
        public string status { get; set; }
    }
}
