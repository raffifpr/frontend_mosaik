using System;
using System.Collections.Generic;
using System.Text;

namespace Mosaik.id.Model
{
    public class AddNewHistoryRequest
    {
        public string email { get; set; }
        public string link { get; set; }
        public string time { get; set; }
        public string date { get; set; }
    }
}
