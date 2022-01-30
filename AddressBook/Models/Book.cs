using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using GalaSoft.MvvmLight;

namespace AddressBook.Models
{
    public class Book : ObservableObject
    {
        public ObservableCollection<Contact> ContactsCollection { get; }

        public IEnumerable<Contact> FilteredCollection
        {
            get
            {
                IEnumerable<Contact> res = ContactsCollection;
                if (!string.IsNullOrEmpty(_filterSurname)) res = res.Where(item => item.Surname.StartsWith(_filterSurname));
                if (!string.IsNullOrEmpty(_filterName)) res = res.Where(item => item.Name.StartsWith(_filterName));
                if (!string.IsNullOrEmpty(_filterSecondName)) res = res.Where(item => item.SecondName.StartsWith(_filterSecondName));
                if (_filterPhoneNumber!=null && _filterPhoneNumber != Properties.Resources.PhoneNumberMask)
                    res = res.Where(item => item.IsPhoneNumberFiltered(_filterPhoneNumber));
                return res;
            }
        }

        public int CountId { get; set; }

        private string _filterSurname;
        public string FilterSurname
        {
            get => _filterSurname;
            set
            {
                _filterSurname = value;
                RaisePropertyChanged(nameof(FilteredCollection));
            }
        }

        private string _filterName;
        public string FilterName
        {
            get => _filterName;
            set
            {
                _filterName = value;
                RaisePropertyChanged(nameof(FilteredCollection));
            }
        }

        private string _filterSecondName;
        public string FilterSecondName
        {
            get => _filterSecondName;
            set
            {
                _filterSecondName = value;
                RaisePropertyChanged(nameof(FilteredCollection));
            }
        }

        private string _filterPhoneNumber;
        public string FilterPhoneNumber
        {
            get => _filterPhoneNumber;
            set
            {
                _filterPhoneNumber = value;
                RaisePropertyChanged(nameof(FilteredCollection));
            }
        }

        public Book()
        {
            ContactsCollection = new ObservableCollection<Contact>();
        }

        public void AddContact(Contact newContact)
        {
            newContact.Id = ++CountId;
            ContactsCollection.Add(newContact);
        }
    }
}
