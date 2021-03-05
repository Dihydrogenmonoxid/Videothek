using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;

namespace Videothek.Logic.Ui.ViewModel
{
	/// <summary>
	/// This class contains static references to all the view models in the
	/// application and provides an entry point for the bindings.
	/// </summary>
	public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

			if (ViewModelBase.IsInDesignModeStatic)
			{
				// Create design time view services and models
			}
			else
			{
				// Create run time view services and models
			}

			SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<HauptFensterViewModel>();
            SimpleIoc.Default.Register<BearbeitenViewModel>();
            SimpleIoc.Default.Register<DbAbfragen>();
        }

        public MainViewModel Main => ServiceLocator.Current.GetInstance<MainViewModel>();
        public HauptFensterViewModel HauptFenster => ServiceLocator.Current.GetInstance<HauptFensterViewModel>();
        public BearbeitenViewModel Bearbeiten => ServiceLocator.Current.GetInstance<BearbeitenViewModel>();
        public DbAbfragen DbAbfragen => ServiceLocator.Current.GetInstance<DbAbfragen>();
        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}