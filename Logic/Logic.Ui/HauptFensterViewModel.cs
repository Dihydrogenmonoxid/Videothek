using GalaSoft.MvvmLight;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using System.Data;
using System.Data.SqlClient;
using GalaSoft.MvvmLight.Messaging;
using System.Windows;

namespace Videothek.Logic.Ui.ViewModel
{
    public class HauptFensterViewModel : ViewModelBase
    {
  //      string Table = "";
  //      private SqlConnection conn = new SqlConnection();

  //      private DataView selectedData;
  //      public DataView SelectedData
  //      {
  //          get => selectedData;
  //          set
  //          {
  //              selectedData = value;
  //              RaisePropertyChanged("SelectedData");
  //          }
  //      }
  //      private ICommand _onTableSelect;

  //      public ICommand OnTableSelect
  //      {
  //          get
  //          {
  //              if (_onTableSelect == null)
  //              {
  //                  _onTableSelect = new RelayCommand<string>((table) =>
  //                  {
  //                      SelectedData = GetDataViewOf(table);
  //                  });
  //              }
                
  //              return _onTableSelect;
  //          }
  //      }
  //      /// <summary>
  //      /// Initializes a new instance of the MainViewModel class.
  //      /// </summary>
  //      public HauptFensterViewModel()
		//{
		//	if (IsInDesignMode)
		//	{
		//		// Code runs in Blend --> create design time data.
		//	}
		//	else
		//	{
		//		// Code runs "for real"
		//	}
		//}
  //      private DataView GetDataViewOf(string table)
  //      {
  //          Table = table;
  //          DataTable dt = new DataTable();
  //          conn.ConnectionString = "Data Source = DESKTOP-76G58PK\\AJMYSQLSERVER;" +
  //                                  "Initial Catalog=Videothek;" +
  //                                  "Integrated Security=SSPI";
  //          SqlDataAdapter adapter = new SqlDataAdapter();

  //          try
  //          {
  //              conn.Open();
  //              adapter.SelectCommand = new SqlCommand("SELECT * FROM " + table, conn);
  //              adapter.Fill(dt);
  //              return dt.DefaultView;
  //          }
  //          catch (System.Exception e)
  //          {
  //              conn.Close();
  //              throw e;
  //          }
  //          finally
  //          {
  //              conn.Close();
  //          }
  //      }
  //      interface IDialogService
  //      {
  //          void ShowMessageBox(string message);
  //      }

  //      private ICommand _HinzufügenKlick;
  //      public ICommand HinzufügenKlick
  //      {
  //          get
  //          {
  //              if (_HinzufügenKlick == null)
  //              {
  //                      _HinzufügenKlick = new RelayCommand(() =>
  //                      {
  //                          if (Table != "")
  //                          {
  //                              var l = new HauptFensterResult() { Result = true };
  //                              var p = new PropertyChangedMessage<HauptFensterResult>(null, l, Table);
  //                              Messenger.Default.Send(p);
  //                          }
  //                      });
  //              }

  //              return _HinzufügenKlick;
  //          }

  //      }

  //      private ICommand _BearbeitenKlick;
  //      public ICommand BearbeitenKlick
  //      {
  //          get
  //          {
  //              if (_BearbeitenKlick == null)
  //              {
  //                  _BearbeitenKlick = new RelayCommand(() =>
  //                  {
  //                      if (Table != "")
  //                      {
  //                          var l = new HauptFenster2Result() { Result = true };
  //                          var p = new PropertyChangedMessage<HauptFenster2Result>(null, l, Table);
  //                          Messenger.Default.Send(p);
  //                      }
  //                  });
  //              }

  //              return _BearbeitenKlick;
  //          }

  //      }

    }

}