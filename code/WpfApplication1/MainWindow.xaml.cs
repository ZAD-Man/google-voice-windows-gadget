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
        private static uint _textPageNumber = 1;
        private readonly GoogleVoice voice = new GoogleVoice();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            string userName = UsernameField.Text;
            string password = PasswordField.Password;
                
            try
            {
                LoginResult result = voice.Login(userName, password);
                bool newResult = !result.RequiresRelogin;
                MessageBox.Show(newResult.ToString());
                TextTab.Focusable = true;
                MessagesTab.Focusable = true;
                closeTabItem(loginTab);
            }
            catch (Exception)
            {
                LoginError.Visibility = Visibility.Visible;
                PasswordField.Clear();
            }
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
            string key = DestinationNumber.Text;
            foreach (var v in voice.GetContacts())
            {
                if (v.Value.name.Contains(key) || v.Value.phoneNumber.Contains(key))
                {
                    FilterResult.Add(v.Value);
                }
            }
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            updateTexts(voice.Texts(_textPageNumber).messages);
        }

        private void LastPageButton_Click(object sender, RoutedEventArgs e)
        {
            if (_textPageNumber > 1)
            {
                _textPageNumber--;
                updateTexts(voice.Texts(_textPageNumber).messages);
            }
        }

        private void NextPageButton_Click(object sender, RoutedEventArgs e)
        {
            _textPageNumber++; //TODO: Check if at end page
            updateTexts(voice.Texts(_textPageNumber).messages);
        }

        private void updateTexts(Dictionary<string, Message> messageDictionary)
        {
            var messages = new Message[messageDictionary.Count];
            int counter = 0;
            foreach (var message in messageDictionary)
            {
                messages[counter] = message.Value;
                counter++;
            }

            //TODO: More efficient sort? It's 10 items max, so it doesn't really matter
            for (int i = 0; i < messages.Length; i++)
            {
                for (int j = 0; j < messages.Length - 1; j++)
                {
                    if (messages[j].startTime < messages[j + 1].startTime)
                    {
                        var temp = messages[j];
                        messages[j] = messages[j + 1];
                        messages[j + 1] = temp;
                    }
                }
            }

            //TODO: Have this work according to how many texts are on the page.
            //Current problem - don't know how to get it to choose which Grid, etc. to update if it's in a for loop. Ideally there wouldn't be a set number of TextXGrids, even though it will never be more than 10, but they would be added to the StackPanel as needed.
            ContactNumber1.Content = messages[0].displayNumber;
            Message1.Content = messages[0].messageText;
            Text1Grid.Visibility = Visibility.Visible;

            ContactNumber2.Content = messages[1].displayNumber;
            Message2.Content = messages[1].messageText;
            Text2Grid.Visibility = Visibility.Visible;

            ContactNumber3.Content = messages[2].displayNumber;
            Message3.Content = messages[2].messageText;
            Text3Grid.Visibility = Visibility.Visible;

            ContactNumber4.Content = messages[3].displayNumber;
            Message4.Content = messages[3].messageText;
            Text4Grid.Visibility = Visibility.Visible;

            ContactNumber5.Content = messages[4].displayNumber;
            Message5.Content = messages[4].messageText;
            Text5Grid.Visibility = Visibility.Visible;

            ContactNumber6.Content = messages[5].displayNumber;
            Message6.Content = messages[5].messageText;
            Text6Grid.Visibility = Visibility.Visible;

            ContactNumber7.Content = messages[6].displayNumber;
            Message7.Content = messages[6].messageText;
            Text7Grid.Visibility = Visibility.Visible;

            ContactNumber8.Content = messages[7].displayNumber;
            Message8.Content = messages[7].messageText;
            Text8Grid.Visibility = Visibility.Visible;

            ContactNumber9.Content = messages[8].displayNumber;
            Message9.Content = messages[8].messageText;
            Text9Grid.Visibility = Visibility.Visible;

            ContactNumber10.Content = messages[9].displayNumber;
            Message10.Content = messages[9].messageText;
            Text10Grid.Visibility = Visibility.Visible;

        }
    }
}