using GalaSoft.MvvmLight.Messaging;
using System;
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
using Videothek.Logic.Ui.ViewModel;

namespace Videothek.Ui.Desktop
{
	/// <summary>
	/// Interaction logic for Artikel.xaml
	/// </summary>
	public partial class Artikel : UserControl
	{
		public Artikel()
		{
			InitializeComponent();

            Messenger.Default.Register<PropertyChangedMessage<AddResult1>>(this, (PropertyChangedMessage<AddResult1> e) =>
            {
                if (e.PropertyName.Equals("PickCategory"))
                {
                    PickData pick = new PickData();
                    ChildWindowPickData childWindowPickData = new ChildWindowPickData();
                    childWindowPickData.Anzeiger3.Content = pick;
                    childWindowPickData.Show();
                }
            });
        }
	}
}
