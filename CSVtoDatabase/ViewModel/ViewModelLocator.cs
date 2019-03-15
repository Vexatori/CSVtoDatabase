/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:CSVtoDatabase"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using CommonServiceLocator;

using CSVtoDatabase.Context;
using CSVtoDatabase.Interfaces;

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;

namespace CSVtoDatabase.ViewModel
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<MainWindowViewModel>();

            SimpleIoc.Default.Register<IUsersData, EFUsersData>();

            SimpleIoc.Default.Register<UsersDatabaseContext>();
        }

        public MainWindowViewModel MainWindowViewModel => ServiceLocator.Current.GetInstance<MainWindowViewModel>();

        public static void Cleanup()
        {

        }
    }
}