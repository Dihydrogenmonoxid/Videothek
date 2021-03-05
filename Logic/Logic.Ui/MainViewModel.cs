using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using MySqlX.XDevAPI.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static Videothek.Logic.Ui.DbAbfragen;

namespace Videothek.Logic.Ui.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
     

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        #region 
        {
            if (IsInDesignMode)
			{
				// Code runs in Blend --> create design time data.
			}
			else
			{
				// Code runs "for real"
			}
		}
        #endregion Commands

        #region Properties
        //Properties umverlagern zu HauptFensterViewModel
        private string _ChildWindowTitle;
        public string ChildWindowTitle
        {
            get
            {
                return _ChildWindowTitle;
            }
            set
            {
                _ChildWindowTitle = value;
                RaisePropertyChanged("ChildWindowTitle");
            }
        }
        //private int _ID;
        //public int ID
        //{
        //    get
        //    {
        //        return _ID;
        //    }
        //    set
        //    {
        //        _ID = value;
        //        RaisePropertyChanged("ID");
        //    }
        //}
        //private string _Vorname;
        //public string Vorname
        //{
        //    get
        //    {
        //        return _Vorname;
        //    }
        //    set
        //    {
        //        _Vorname = value;
        //        RaisePropertyChanged("Vorname");
        //    }
        //}
        //private string _Name;
        //public string Name
        //{
        //    get
        //    {
        //        return _Name;
        //    }
        //    set
        //    {
        //        _Name = value;
        //        RaisePropertyChanged("Name");
        //    }
        //}
        //private string _Strasse;
        //public string Strasse
        //{
        //    get
        //    {
        //        return _Strasse;
        //    }
        //    set
        //    {
        //        _Strasse = value;
        //        RaisePropertyChanged("Strasse");
        //    }
        //}
        //private string _Hausnummer;
        //public string Hausnummer
        //{
        //    get
        //    {
        //        return _Hausnummer;
        //    }
        //    set
        //    {
        //        _Hausnummer = value;
        //        RaisePropertyChanged("Hausnummer");
        //    }
        //}
        //private int _PLZ;
        //public int PLZ
        //{
        //    get
        //    {
        //        return _PLZ;
        //    }
        //    set
        //    {
        //        _PLZ = value;
        //        RaisePropertyChanged("PLZ");
        //    }
        //}
        //private string _Ort;
        //public string Ort
        //{
        //    get
        //    {
        //        return _Ort;
        //    }
        //    set
        //    {
        //        _Ort = value;
        //        RaisePropertyChanged("Ort");
        //    }
        //}
        private string _Bezeichnung;
        public string Bezeichnung
        {
            get
            {
                return _Bezeichnung;
            }
            set
            {
                _Bezeichnung = value;
                RaisePropertyChanged("Bezeichnung");
            }
        }
        private string _Abgabedatum;
        public string Abgabedatum
        {
            get
            {
                return _Abgabedatum;
            }
            set
            {
                _Abgabedatum = value;
                RaisePropertyChanged("Abgabedatum");
            }
        }
        private string _Leihdatum;
        public string Leihdatum
        {
            get
            {
                return _Leihdatum;
            }
            set
            {
                _Leihdatum = value;
                RaisePropertyChanged("Leihdatum");
            }
        }
        #endregion Properties

        DbAbfragen db = new DbAbfragen();
        private SqlConnection conn = new SqlConnection();

        #region Startseite Weiterleitung
        private ICommand _StartKlick;
			public ICommand StartKlick
        {
            get
            {
				if (_StartKlick == null)
                {
					_StartKlick = new RelayCommand(() =>
					{
					    var l = new StartSeiteResult() { Result = true };
						var p = new PropertyChangedMessage<StartSeiteResult>(null, l, "StartKlick");
						Messenger.Default.Send(p);
					});                }

				return _StartKlick;
            }
        }
        #endregion Startseite Weiterleitung

        #region Bearbeiten
        // Property für die Tabelle in HauptFenster
        private List<dynamic> selectedData;
        public List<dynamic> SelectedData
        {
            get => selectedData;
            set
            {
                selectedData = value;
                RaisePropertyChanged("SelectedData");
            }
        }

        // Wenn Tabelle in Menü ausgewählt (SelectedData)
        string Table = "";
        private ICommand _onTableSelect;

        public ICommand OnTableSelect
        {
            get
            {
                if (_onTableSelect == null)
                {
                    _onTableSelect = new RelayCommand<string>((table) =>
                    {
                        Table = table;
                        switch (table)
                        {
                            case "Kunde":
                                SelectedData = db.GetAllCustomers().Cast<dynamic>().ToList();
                                break;
                            case "Artikel":
                                SelectedData = db.GetAllArticles().Cast<dynamic>().ToList();
                                break;
                            case "Kategorie":
                                //List<KategorieClass>
                                SelectedData = db.GetAllCategories().Cast<dynamic>().ToList();
                                break;
                            case "Art_Ausgeliehen":
                                SelectedData = db.GetAllArt_Ausgeliehen().Cast<dynamic>().ToList();
                                break;
                        }
                    });
                }

                return _onTableSelect;
            }
        }
        

        //Properties --> Wenn in die Tabelle geklickt wurde (für Bearbeiten)
        private dynamic _selectedGridColumn;
        public dynamic selectedGridColumn
        {
            get => _selectedGridColumn;
            set
            {
                _selectedGridColumn = value;
                MyCustomer = value;
                RaisePropertyChanged("SelectedGridColumn");
            }
        }

        //Wenn (im HauptFenster.xaml) auf Bearbeiten gedrückt wurde
        private ICommand _BearbeitenKlick;
        public ICommand BearbeitenKlick
        {
            get
            {
                if (_BearbeitenKlick == null)
                {
                    _BearbeitenKlick = new RelayCommand(() =>
                    {
                        if (Table != "")
                        {
                            if (Table == "Art_Ausgeliehen") ChildWindowTitle = "Ausgeliehener Artikel"; 
                            else ChildWindowTitle = Table; 
                            WurdeHinzufügenGeklickt = false;
                            var l = new BearbeitenResult() { Result = true };
                            var p = new PropertyChangedMessage<BearbeitenResult>(null, l, Table);
                            Messenger.Default.Send(p);
                        }
                    });
                }

                return _BearbeitenKlick;
            }

        }

        // Wenn hier auf Content(in Kunde.xaml) gedrückt wurde 

        private ICommand _EditOrAddCustomer;
        public ICommand EditOrAddCustomer
        {
            get
            {
                if (_EditOrAddCustomer == null)
                {
                    _EditOrAddCustomer = new RelayCommand(() =>
                    {
                        if (Table != "")
                        {
                            //Ist vorher Hinzufügen oder Bearbeiten gedrückt worden?
                            if (db.AddOrEditCustomer(MyCustomer.ID, MyCustomer.Name, MyCustomer.Vorname,
                                MyCustomer.Strasse, MyCustomer.Hausnummer, MyCustomer.PLZ, MyCustomer.Ort, WurdeHinzufügenGeklickt))
                            {
                                var l = new EditResult() { EditSuccess = true };
                                var p = new PropertyChangedMessage<EditResult>(null, l, "EditOrAddCustomer");
                                Messenger.Default.Send<PropertyChangedMessage<EditResult>>(p);
                            }
                            else
                            {
                                var l = new EditResult() { EditSuccess = false };
                                var p = new PropertyChangedMessage<EditResult>(null, l, "EditOrAddCustomer");
                                Messenger.Default.Send<PropertyChangedMessage<EditResult>>(p);
                            }
                            SelectedData = db.GetAllCustomers().Cast<dynamic>().ToList();
                        }
                    });
                }
                return _EditOrAddCustomer;
            }
        }
        #endregion Bearbeiten
        // Warum? Wenn man auf Content drückt erwartet er bei beiden (Hinzufügen & Bearbeiten) etwas Anderes)
        #region Hinzufügen

        // Wenn in (HauptFenster.xaml) auf Hinzufügen geklickt wurde
        bool WurdeHinzufügenGeklickt;
        private ICommand _HinzufügenKlick;
        public ICommand HinzufügenKlick
        {
            get
            {
                if (_HinzufügenKlick == null)
                {
                    _HinzufügenKlick = new RelayCommand(() =>
                    {
                        if (Table != "") 
                        {
                            if (Table == "Art_Ausgeliehen") ChildWindowTitle = "Ausgeliehener Artikel";
                            else ChildWindowTitle = Table;
                            WurdeHinzufügenGeklickt = true;
                            var l = new BearbeitenResult() { Result = true };
                            var p = new PropertyChangedMessage<BearbeitenResult>(null, l, Table);
                            Messenger.Default.Send(p);
                        }
                    });
                }

                return _HinzufügenKlick;
            }

        }
        #region Codeabschnitt von Paul 
        CustomerClass CC = new CustomerClass();


        private CustomerClass _MyCustomer;
        public CustomerClass MyCustomer
        {
            get { return customerClassNull(_MyCustomer); }
            set { _MyCustomer = value; }
        }

        private CustomerClass customerClassNull(CustomerClass value)
        {
            if (value == null)
            {
                CC.ID = 0;
                CC.Name = "";
                CC.Vorname = "";
                CC.Strasse = "";
                CC.Hausnummer = "";
                CC.PLZ = 0;
                CC.Ort = "";
                return _MyCustomer = CC;
            }
            else
            {
                return value;
            }
        }
        #endregion

        // Wenn hier auf Content(in Kunde.xaml) gedrückt wurde 

        private ICommand _AddCustomer;
        public ICommand AddCustomer
        {
            get
            {
                if (_AddCustomer == null)
                {
                    _AddCustomer = new RelayCommand(() =>
                    {
                        if (Table != "")
                        {
                            //Ist vorher Hinzufügen oder Bearbeiten gedrückt worden?
                            WurdeHinzufügenGeklickt = true;
                            if (db.AddOrEditCustomer(selectedGridColumn.ID, selectedGridColumn.Name, selectedGridColumn.Vorname, selectedGridColumn.Strasse, selectedGridColumn.Hausnummer, selectedGridColumn.PLZ, selectedGridColumn.Ort, WurdeHinzufügenGeklickt))
                            {
                                var l = new AddResult() { AddSuccess = true };
                                var p = new PropertyChangedMessage<AddResult>(null, l, "AddCustomer");
                                Messenger.Default.Send(p);
                            }
                            else
                            {
                                var l = new AddResult() { AddSuccess = false };
                                var p = new PropertyChangedMessage<AddResult>(null, l, "AddCustomer");
                                Messenger.Default.Send<PropertyChangedMessage<AddResult>>(p);
                            }
                        }
                    });
                }
                return _AddCustomer;
            }
        }
        /// <summary>
        /// ///////////////////////////////////////////
        /// </summary>
        /// 
        #endregion Hinzufügen

        #region Delete
        // Property für die Tabelle in HauptFenster
        private DataView _SelectedDataToDelete;
        public DataView SelectedDataToDelete
        {
            get => _selectedGridColumn;
            set
            {
                _SelectedDataToDelete = value;
                RaisePropertyChanged("SelectedDataToDelete");
            }
        }


        private ICommand _Delete;
        public ICommand Delete
        {
            get
            {
                if (_Delete == null)
                {
                    _Delete = new RelayCommand(() =>
                    {
                        if (Table != "")
                        {
                            SelectedDataToDelete = db.ShowColumnWhichShouldBeDeleted(MyCustomer.ID, Table);
                            //Opens the DeleteYesOrNo.xaml Window
                            var l = new EditResult() { EditSuccess = true };
                            var p = new PropertyChangedMessage<EditResult>(null, l, "Delete");
                            Messenger.Default.Send<PropertyChangedMessage<EditResult>>(p);

                            ////Ist vorher Hinzufügen oder Bearbeiten gedrückt worden?
                            //if (db.DeleteCustomer(SelectedGridColumn.ID, SelectedGridColumn.Name, SelectedGridColumn.Vorname, SelectedGridColumn.Strasse, SelectedGridColumn.Hausnummer, SelectedGridColumn.PLZ, SelectedGridColumn.Ort, WurdeHinzufügenGeklickt))
                            //{
                            //    var l = new EditResult() { EditSuccess = true };
                            //    var p = new PropertyChangedMessage<EditResult>(null, l, "Delete");
                            //    Messenger.Default.Send<PropertyChangedMessage<EditResult>>(p);
                            //}
                            //else
                            //{
                            //    var l = new EditResult() { EditSuccess = false };
                            //    var p = new PropertyChangedMessage<EditResult>(null, l, "Delete");
                            //    Messenger.Default.Send<PropertyChangedMessage<EditResult>>(p);
                            //}
                        }
                    });
                }
                return _Delete;
            }
        }

        private ICommand _Yes;
        public ICommand Yes
        {
            get
            {
                if (_Yes == null)
                {
                    _Yes = new RelayCommand(() =>
                    {
                        //Angezeigten Datensatz löschen und Fenster schließen
                        if (db.DeleteCustomer(MyCustomer.ID))
                        {

                        }
                    });
                }
                return _Yes;
            }
        }
        #endregion Delete

        #region Artikel Weiterleitungen
        //Artikel wurde ausgewählt und auf Hinzufügen gedrückt
        private DataView selectedData1;
        public DataView SelectedData1
        {
            get => selectedData1;
            set
            {
                selectedData1 = value;
                RaisePropertyChanged("SelectedData1");
            }
        }

        private ICommand _PickCategory;
        public ICommand PickCategory
        {
            get
            {
                if (_PickCategory == null)
                {
                    _PickCategory = new RelayCommand(() =>
                    {
                        SelectedData1 = db.PickCategory("Bezeichnung", "", "", "", "", "Kategorie");
                        
                            var l = new AddResult1() { AddSuccess = true };
                            var p = new PropertyChangedMessage<AddResult1>(null, l, "PickCategory");
                            Messenger.Default.Send<PropertyChangedMessage<AddResult1>>(p);
                        
                    });
                }
                return _PickCategory;
            }
        }



        #endregion Artikel Weiterleitungen

        #region Sonstiges
        private ICommand _AddCategory;
        public ICommand AddCategory
        {
            get
            {
                if (_AddCategory == null)
                {
                    _AddCategory = new RelayCommand(() =>
                    {
                        if (db.AddCategory(Bezeichnung))
                        {
                            var l = new AddResult2() { AddSuccess = true };
                            var p = new PropertyChangedMessage<AddResult2>(null, l, "AddCategory");
                            Messenger.Default.Send<PropertyChangedMessage<AddResult2>>(p);
                        }
                        else
                        {
                            var l = new AddResult2() { AddSuccess = false };
                            var p = new PropertyChangedMessage<AddResult2>(null, l, "AddCategory");
                            Messenger.Default.Send<PropertyChangedMessage<AddResult2>>(p);
                        }
                    });
                }
                return _AddCategory;
            }
        }
        #endregion Sonstiges
        #region Artikel_Ausgeliehen
        private ICommand _PickCustomerID;
        public ICommand PickCustomerID
        {
            get
            {
                if (_PickCustomerID == null)
                {
                    _PickCustomerID = new RelayCommand(() =>
                    {
                        SelectedData1 = db.PickCategory("ID, ", "Name, ", "Vorname, ", "Strasse, ", "Hausnummer ", "Kunde ");

                        var l = new AddResult1() { AddSuccess = true };
                        var p = new PropertyChangedMessage<AddResult1>(null, l, "PickCustomerID");
                        Messenger.Default.Send<PropertyChangedMessage<AddResult1>>(p);
                    });
                }
                return _PickCustomerID;
            }
        }

        private ICommand _PickArtikelID;
        public ICommand PickArtikelID
        {
            get
            {
                if (_PickArtikelID == null)
                {
                    _PickArtikelID = new RelayCommand(() =>
                    {
                        SelectedData1 = db.PickArtikelID();

                        var l = new AddResult1() { AddSuccess = true };
                        var p = new PropertyChangedMessage<AddResult1>(null, l, "PickArtikelID");
                        Messenger.Default.Send<PropertyChangedMessage<AddResult1>>(p);
                    });
                }
                return _PickArtikelID;
            }
        }

        //private ICommand _AddAusgeliehenerArtikel;
        //public ICommand AddAusgeliehenerArtikel
        //{
        //    get
        //    {
        //        if (_AddAusgeliehenerArtikel == null)
        //        {
        //            _AddAusgeliehenerArtikel = new RelayCommand(() =>
        //            {
        //                if (db.AddCustomer(SelectedGridColumn.Name, SelectedGridColumn.Vorname, SelectedGridColumn.Strasse, SelectedGridColumn.Hausnummer, SelectedGridColumn.PLZ, SelectedGridColumn.Ort, WurdeHinzufügenGeklickt, SelectedGridColumn.Bezeichnung, SelectedGridColumn.Menge, SelectedGridColumn.Leihpreis))
        //                {
        //                    var l = new AddResult() { AddorEditSuccess = true };
        //                    var p = new PropertyChangedMessage<AddResult>(null, l, "AddAusgeliehenerArtikel");
        //                    Messenger.Default.Send<PropertyChangedMessage<AddResult>>(p);
        //                }
        //                else
        //                {
        //                    var l = new AddResult() { AddorEditSuccess = false };
        //                    var p = new PropertyChangedMessage<AddResult>(null, l, "AddAusgeliehenerArtikel");
        //                    Messenger.Default.Send<PropertyChangedMessage<AddResult>>(p);
        //                }
        //            });
        //        }
        //        return _AddAusgeliehenerArtikel;
        //    }
        //}
        #endregion Artikel_Ausgeliehen
    }
    #region Klassen zum Anzeigen der Tabellen
    public class CustomerClass
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Vorname { get; set; }
        public string Strasse { get; set; }
        public string Hausnummer { get; set; }
        public int PLZ { get; set; }
        public string Ort { get; set; }
    }

    public class ArtikelClass
    {
        public int ID { get; set; }
        public string Bezeichnung { get; set; }
        public int Menge { get; set; }
        public decimal Leihpreis { get; set; }
    }
    public class KategorieClass
    {
        public int ID { get; set; }
        public string Bezeichnung { get; set; }
    }
    public class Artikel_AusgeliehenClass
    {
        public int ID { get; set; }
        public int PickCustomerID { get; set; }
        public int ArtikelID { get; set; }
        public string Bezeichnung { get; set; }
        public DateTime Abgabedatum { get; set; }
        public DateTime Leihdatum { get; set; }
        public string Name { get; set; }
        public string Vorname { get; set; }
    }
    #endregion Klassen zum Anzeigen der Tabellen


    #region Results
    public class HauptFensterResult
	{
		public bool? Result { get; set; }
	}
    public class BearbeitenResult
    {
        public bool? Result { get; set; }
    }
    public class StartSeiteResult
    {
		public bool? Result { get; set; }
    }
    public class AddResult
    {
        public bool? AddSuccess { get; set; }
    }
    public class EditResult
    {
        public bool? EditSuccess { get; set; }
    }
    public class AddResult1
    {
        public bool? AddSuccess { get; set; }
    }
    public class AddResult2
    {
        public bool? AddSuccess { get; set; }
    }
    #endregion Results
}