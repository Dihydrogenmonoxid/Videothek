using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using MySqlX.XDevAPI.Common;
using System.Windows.Input;

namespace Videothek.Logic.Ui.ViewModel
{
    public class BearbeitenViewModel : ViewModelBase
    {
       
        public BearbeitenViewModel()
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
	}
}