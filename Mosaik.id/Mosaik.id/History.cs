using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Mosaik.id
{
    public class Person
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Link { get; set; }
        public string AccessedTime { get; set; }
    }
}
