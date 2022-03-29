using System;
using System.Collections.Generic;
using System.Text;

namespace Mosaik.idAPI.Models
{
    public class MosaikHistory
    {
        public int MosaikHistoryID { get; set; }
        public int MosaikDateHistoryID {get; set;}
        public int userID { get; set; }
        public string Link { get; set; }
        public string AccessedTime { get; set; }
    }
}
