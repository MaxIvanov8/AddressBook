using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;

namespace AddressBook.ViewModels
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<AddContactVM>();
        }
        public MainViewModel Main => ServiceLocator.Current.GetInstance<MainViewModel>();
        public AddContactVM AddContact => ServiceLocator.Current.GetInstance<AddContactVM>();

    }
}
