using System;
using System.Collections.Generic;
using System.Text;

namespace Mosaik.id.Model
{
    public class ChangePasswordResponse
    {
        // Status = "wrong password", "success", "failed"
        public string Status { get; set; }
    }
}
