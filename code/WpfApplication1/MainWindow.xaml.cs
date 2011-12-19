using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using Google.Voice;
using Google.Voice.Entities;
using Google.Voice.Web;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly GoogleVoice voice = new GoogleVoice();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            string _userName = UsernameField.Text;
            string _password = PasswordField.Password;
            LoginResult result = voice.Login(_userName, _password);
            bool newResult = !result.RequiresRelogin;
            MessageBox.Show(newResult.ToString());
            closeTabItem(loginTab);
        }

        private void closeTabItem(TabItem item)
        {
            if (item != null)
            {
                // find the parent tab control
                var tabControl = item.Parent as TabControl;
                if (tabControl != null)
                    tabControl.Items.Remove(item); // remove tabItem
            }
        }


        private void SendTextButton_Click(object sender, RoutedEventArgs e)
        {
            string phoneNr = DestinationNumber.Text;
            string message = messageField.Text;
            bool isSent = voice.SMS(phoneNr, message);
            MessageBox.Show(isSent.ToString());
            messageField.Clear();
        }

        private ObservableCollection<Contact> FilterResult = new ObservableCollection<Contact>();

        private void DestinationNumber_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Contact contact = new Contact();

            string key = DestinationNumber.Text;
            foreach (var v in voice.GetContacts())
            {
                if (contact.name.Contains(key) || contact.phoneNumber.Contains(key))
                {
                    FilterResult.Add(v.Value);
                }
            }
        }

    }
}