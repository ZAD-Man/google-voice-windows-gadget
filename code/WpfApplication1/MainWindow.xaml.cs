using System;
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
            SendTab.IsEnabled = false;
            MessageTab.IsEnabled = false;
            ContactTab.IsEnabled = false;
            

        }

        public List<DropDownList> fetchList()
        {
            List<DropDownList> objDLL = new List<DropDownList>();
            foreach (var v in voice.GetContacts())
            {
                objDLL.Add(new DropDownList(){Item = v.Value.name, value = v.Value.displayNumber});
            }
            return objDLL;
        }


        public class DropDownList
        {
            public String Item { get; set; }
            public String value { get; set; }
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            string _userName = UsernameField.Text;
            string _password = PasswordField.Password;
            LoginResult result = voice.Login(_userName, _password);
            bool newResult = !result.RequiresRelogin;
            MessageBox.Show(newResult.ToString());
            closeTabItem(loginTab);
            SendTab.IsEnabled = true;
            MessageTab.IsEnabled = true;
            ContactTab.IsEnabled = true;
            DestinationNumber.ItemsSource = fetchList();
            DestinationNumber.DisplayMemberPath = "Item";
            DestinationNumber.SelectedValuePath = "value";
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
            string phoneNr = DestinationNumber.SelectedValue.ToString();
            string message = messageField.Text;
            bool isSent = voice.SMS(phoneNr, message);
            if (isSent)
            {
            MessageBox.Show("Message sent :)");
                
            }
            else
            {
                MessageBox.Show("Message not sent :(");
            }
            messageField.Clear();
        }


        private void DestinationNumber_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            foreach (var v in voice.GetContacts())
            {
                MessageBox.Show(v.Value.displayNumber);
            }
        }

        private void comboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var lab = "Selected value " + DestinationNumber.SelectedValue;
            label1.Content = lab;
        }



    }
}