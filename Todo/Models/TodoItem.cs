using System;
using System.ComponentModel;
using Geolocator.Plugin;
using SQLite;

namespace Todo
{
	public class TodoItem: INotifyPropertyChanged
	{

		public event PropertyChangedEventHandler PropertyChanged;

		public TodoItem ()
		{
		}

		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }
		public string Name { get; set; }
		public double Lat { get; set; }
		public double Long { get; set; }
		public bool Done { get; set; }
	}
}

