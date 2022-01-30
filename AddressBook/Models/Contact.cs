using GalaSoft.MvvmLight;

namespace AddressBook.Models
{
    public class Contact : ObservableObject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string SecondName { get; set; }
        public string PhoneNumber { get; set; }

        public Contact()
        {
            Name = string.Empty;
            Surname = string.Empty;
            SecondName = string.Empty;
            PhoneNumber = string.Empty;
        }

        public bool IsValidated =>
            IsValidatedString(Name) && IsValidatedString(Surname) && PhoneNumber!=string.Empty && !PhoneNumber.Contains('_');
        private static bool IsValidatedString(string field) => field!=string.Empty&& field.Length is <= 50 and >= 2;

        public bool IsPhoneNumberFiltered(string filter)
        {
            for (var i = 3; i < PhoneNumber.Length; ++i)
            {
                if (PhoneNumber[i] == filter[i]) continue;
                if (filter[i] != '_' && filter[i] != '-')
                    return false;
            }
            return true;
        }

    }
}
