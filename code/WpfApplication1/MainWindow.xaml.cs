using System.Windows;
using Google.Voice;

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
            voice.Login(_userName, _password);
        }



        private void tabControl1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
