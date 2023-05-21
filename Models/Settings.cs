using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CatanRollTracker.Models
{
    public class Settings : INotifyPropertyChanged
	{
		private bool _autoSaveOnExit = true;
        public bool AutoSaveOnExit
		{
			get { return _autoSaveOnExit; }
			set
			{
				//MessageBox.Show(value.ToString());
				_autoSaveOnExit = value;
				OnPropertyChanged("AutoSaveOnExit");
			}
		}

		protected virtual void OnPropertyChanged(string property)
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(property));
		}

		#region INotifyPropertyChanged Members

		public event PropertyChangedEventHandler PropertyChanged;

		#endregion
	}
}
