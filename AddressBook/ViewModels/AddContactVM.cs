using System.Windows.Input;
using AddressBook.Models;
using AddressBook.Views;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;

namespace AddressBook.ViewModels
{
    public class AddContactVM:ViewModelBase
    {
        private bool _isEdit;
        public Contact NewContact { get; set; }
        public ICommand AddCommand { get; }
        public ICommand ClosingCommand { get; }

        public AddContactVM()
        {
            NewContact = new Contact();

            ClosingCommand = new RelayCommand(ClosingMethod);
            AddCommand = new RelayCommand<ICloseable>(AddMethod);

            Messenger.Default.Register<Contact>(this, nameof(AddContactVM),
               RegisterContact);

        }

        protected void ClosingMethod()
        {
            _isEdit = false;
            NewContact = new Contact();
        }

        private void RegisterContact(Contact contact)
        {
            NewContact = contact;
            _isEdit = true;
        }


        private void AddMethod(ICloseable? window)
        {
            if (string.IsNullOrEmpty(NewContact.PhoneNumber)) NewContact.PhoneNumber = Properties.Resources.PhoneNumberMask;
            if (!_isEdit)
                Messenger.Default.Send(NewContact, nameof(MainViewModel));
            window.Close();
        }
    }
}
