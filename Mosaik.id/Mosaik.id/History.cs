using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using SQLite;

namespace Mosaik.id
{
    public class Person
    {
        public string Link { get; set; }
        public string AccessedTime { get; set; }

		public Person()
        {

        }
    }

	public class GroupedPerson : ObservableCollection<Person>
	{
		public string Date { get; set; }
	}
}
