using System;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Xml;
using System.Xml.Serialization;
using AddressBook.Models;
using AddressBook.Views;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Win32;

namespace AddressBook.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly XmlSerializer _serializer;
        private const string XmlFilter = "Xml files (*.xml)|*.xml";
        public Book AddressBook { get; set; }
        public ICommand OpenCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand AddContactCommand { get; }

        public MainViewModel()
        {
            _serializer = new XmlSerializer(typeof(Book));
            AddressBook = new Book();

            EditCommand = new RelayCommand<Contact>(EditMethod);
            DeleteCommand = new RelayCommand<Contact>(DeleteMethod);
            AddContactCommand = new RelayCommand(AddContactMethod);
            SaveCommand = new RelayCommand(SaveMethod);
            OpenCommand = new RelayCommand(OpenMethod);

            Messenger.Default.Register<Contact>(this, nameof(MainViewModel),
                contact=> AddressBook.AddContact(contact));
        }

        private void SaveMethod()
        {
            var saveFileDialog = new SaveFileDialog
            {
                InitialDirectory = Environment.CurrentDirectory,
                Filter = XmlFilter,
                Title = "Save book",
                FileName = "New book",
                RestoreDirectory = true
            };
            if (saveFileDialog.ShowDialog() == true)
            {
                using var xw = XmlWriter.Create(saveFileDialog.FileName);
                _serializer.Serialize(xw, AddressBook);
            }
        }

        private void OpenMethod()
        {
            var openFileDialog = new OpenFileDialog
            {
                InitialDirectory = Environment.CurrentDirectory,
                Filter = XmlFilter,
                Title = "Open book",
                RestoreDirectory = true
            };
            if (openFileDialog.ShowDialog() == true)
            {
                var reader = new StreamReader(openFileDialog.FileName);
                try
                {
                    AddressBook = (Book)_serializer.Deserialize(reader);
                }
                catch
                {
                    MessageBox.Show("File is damaged", "Program message");
                }
                reader.Close();
            }
        }

        private void DeleteMethod(Contact contact)
        {
            AddressBook.ContactsCollection.Remove(contact);
            RaisePropertyChanged(nameof(AddressBook));
        }
        private static void EditMethod(Contact contact)
        {
            var view = new AddContactView("Edit contact");
            Messenger.Default.Send(contact, nameof(AddContactVM));
            view.ShowDialog();
        }

        private static void AddContactMethod()
        {
            var view = new AddContactView("Add contact");
            view.ShowDialog();
        }
    }
}
