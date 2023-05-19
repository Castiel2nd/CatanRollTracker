using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CatanRollTracker
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		// Arrays are oversized on purpose for easier understanding
		private IntegerProperty[] rolls = new IntegerProperty[13];
		private ProgressBar[] progressBars = new ProgressBar[13];
		private Label[] labels = new Label[13];

		//Commands for keybindings
		private ICommand rollCommand;

		public ICommand Roll2Command
		{
			get
			{
				return rollCommand
					?? (rollCommand = new ActionCommand(() =>
					{
						MessageBox.Show("command");
						roll(2);
					}));
			}
		}
		public ICommand Roll3Command
		{
			get
			{
				return rollCommand
					?? (rollCommand = new ActionCommand(() =>
					{
						roll(3);
					}));
			}
		}
		public ICommand Roll4Command
		{
			get
			{
				return rollCommand
					?? (rollCommand = new ActionCommand(() =>
					{
						roll(4);
					}));
			}
		}
		public ICommand Roll5Command
		{
			get
			{
				return rollCommand
					?? (rollCommand = new ActionCommand(() =>
					{
						MessageBox.Show("5");
						roll(5);
					}));
			}
		}
		public ICommand Roll6Command
		{
			get
			{
				return rollCommand
					?? (rollCommand = new ActionCommand(() =>
					{
						roll(6);
					}));
			}
		}
		public ICommand Roll7Command
		{
			get
			{
				return rollCommand
					?? (rollCommand = new ActionCommand(() =>
					{
						roll(7);
					}));
			}
		}
		public ICommand Roll8Command
		{
			get
			{
				return rollCommand
					?? (rollCommand = new ActionCommand(() =>
					{
						roll(8);
					}));
			}
		}
		public ICommand Roll9Command
		{
			get
			{
				return rollCommand
					?? (rollCommand = new ActionCommand(() =>
					{
						roll(9);
					}));
			}
		}
		public ICommand Roll10Command
		{
			get
			{
				return rollCommand
					?? (rollCommand = new ActionCommand(() =>
					{
						roll(10);
					}));
			}
		}
		public ICommand Roll11Command
		{
			get
			{
				return rollCommand
					?? (rollCommand = new ActionCommand(() =>
					{
						roll(11);
					}));
			}
		}
		public ICommand Roll12Command
		{
			get
			{
				return rollCommand
					?? (rollCommand = new ActionCommand(() =>
					{
						roll(12);
					}));
			}
		}

		public MainWindow()
		{
			for (int i = 0; i < rolls.Length; i++)
			{
				rolls[i] = new IntegerProperty(0);
			}

			WindowState = WindowState.Maximized;
			InitializeComponent();
			DataContext = this;

			progressBars[2] = roll2sPB;
			progressBars[3] = roll3sPB;
			progressBars[4] = roll4sPB;
			progressBars[5] = roll5sPB;
			progressBars[6] = roll6sPB;
			progressBars[7] = roll7sPB;
			progressBars[8] = roll8sPB;
			progressBars[9] = roll9sPB;
			progressBars[10] = roll10sPB;
			progressBars[11] = roll11sPB;
			progressBars[12] = roll12sPB;

			labels[2] = roll2sLbl;
			labels[3] = roll3sLbl;
			labels[4] = roll4sLbl;
			labels[5] = roll5sLbl;
			labels[6] = roll6sLbl;
			labels[7] = roll7sLbl;
			labels[8] = roll8sLbl;
			labels[9] = roll9sLbl;
			labels[10] = roll10sLbl;
			labels[11] = roll11sLbl;
			labels[12] = roll12sLbl;

			// Bindings
			
			for(int i = 2; i < 13; i++)
			{
				Binding lblBinding = new Binding("Value");
				lblBinding.Source = rolls[i];
				labels[i].SetBinding(Label.ContentProperty, lblBinding);
				progressBars[i].SetBinding(ProgressBar.ValueProperty, lblBinding);
			}
			//roll2sPB.SetBinding();
		}

		private int getMaxRollCount()
		{
			int maxRollCount = 0;

			for(int i = 2; i < rolls.Length; i++)
			{
				if (rolls[i].Value > maxRollCount)
				{
					maxRollCount = rolls[i].Value;
				}
			}

			return maxRollCount;
		}

		private void setPBMaximums(int max)
		{
			for (int i = 2; i < rolls.Length; i++)
			{
				progressBars[i].Maximum = max;
			}
		}

		private void roll(int roll)
		{
			MessageBox.Show(roll.ToString());
			int expectedRollCount = rolls[roll].Value + 1;
			if(expectedRollCount > getMaxRollCount())
			{
				setPBMaximums(expectedRollCount);
			}
			rolls[roll].Value = roll;
		}
	}
}
