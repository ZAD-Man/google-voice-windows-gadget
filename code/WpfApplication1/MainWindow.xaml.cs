using System;
using System.Windows;
using Google.Voice;
using System.Windows.Controls;
using Google.Voice.Web;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Google.Voice.GoogleVoice voice = new GoogleVoice();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            string _userName = UsernameField.Text;
            string _password = PasswordField.Password;
            LoginResult result = voice.Login(_userName, _password);
            var newResult = !result.RequiresRelogin;
            MessageBox.Show(newResult.ToString());
            closeTabItem(loginTab);
        }



        private void closeTabItem(TabItem item)
        {
            if (item != null)
            {
                // find the parent tab control
                TabControl tabControl = item.Parent as TabControl;
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
            DestinationNumber.Clear();
            messageField.Clear();

        }
    }
}
