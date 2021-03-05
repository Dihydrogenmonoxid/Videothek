using GalaSoft.MvvmLight.Messaging;
using System.Windows;
using Videothek.Logic.Ui.ViewModel;

namespace Videothek.Ui.Desktop
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			//DataContext = new MainViewModel();

			Messenger.Default.Register<PropertyChangedMessage<StartSeiteResult>>(this, (PropertyChangedMessage<StartSeiteResult> e) =>
			{
				if (e.PropertyName.Equals("StartKlick"))
				{
					if (e.NewValue.Result == true)
					{
						HauptFenster HptFen = new HauptFenster();
						Anzeiger.Content = HptFen;
					}
				}
			});

			
			//Wenn auf 'Hinzufügen' oder 'Bearbeiten' gedrückt wurde
			Messenger.Default.Register(this, (PropertyChangedMessage<BearbeitenResult> e) =>
			{
				if (e.PropertyName.Equals("Kunde"))
				{
					if ((bool)e.NewValue.Result)
					{
						EditCustomer EC = new EditCustomer();
						ChildWindow childWindow = new ChildWindow();
						childWindow.Anzeiger2.Content = EC;
						childWindow.Show();
					}
				}
				else if (e.PropertyName.Equals("Artikel"))
				{
					if ((bool)e.NewValue.Result)
					{
						Artikel artikel = new Artikel();
						ChildWindow childWindow = new ChildWindow();
						childWindow.Anzeiger2.Content = artikel;
						childWindow.Show();
					}
				}
				else if (e.PropertyName.Equals("Kategorie"))
				{
					Kategorie kategorie = new Kategorie();
					ChildWindow childWindow = new ChildWindow();
					childWindow.Anzeiger2.Content = kategorie;
					childWindow.Show();
				}
				else if (e.PropertyName.Equals("Art_Ausgeliehen"))
				{
					Ausgeliehene_Artikel ausgeliehene_Artikel = new Ausgeliehene_Artikel();
					ChildWindow childWindow = new ChildWindow();
					childWindow.Anzeiger2.Content = ausgeliehene_Artikel;
					childWindow.Show();
				}
			});

			Messenger.Default.Register(this, (PropertyChangedMessage<EditResult> e) =>
			{
				if (e.PropertyName.Equals("Delete"))
                {
					DeleteYesOrNo deleteYesOrNo = new DeleteYesOrNo();
					deleteYesOrNo.Show();
                }
			});
			//Messenger.Default.Register(this, (PropertyChangedMessage<HauptFenster2Result> e) =>
			//{
			//	if (e.PropertyName.Equals("Kunde"))
			//	{
			//		if ((bool)e.NewValue.Result)
			//		{
			//			PickData pick = new PickData();
			//			ChildWindowPickData childWindowPickData = new ChildWindowPickData();
			//			childWindowPickData.Anzeiger3.Content = pick;
			//			childWindowPickData.Show();
			//		}
			//	}
			//});

		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
        {
			StartSeite start = new StartSeite();
			Anzeiger.Content = start;
        }
	}
}
