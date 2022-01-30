namespace AddressBook.Views
{
    /// <summary>
    /// Interaction logic for AddContactView.xaml
    /// </summary>
    public partial class AddContactView : ICloseable
    {
        public AddContactView(string title)
        {
            InitializeComponent();
            Title = title;
        }
    }
}
