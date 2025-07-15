using ExamenLenguajesVisuales1.ViewModels;
using System.Windows;

namespace ExamenLenguajesVisuales1.Views
{
    public partial class LoginView : Window
    {
        public LoginView()
        {
            InitializeComponent();
            DataContext = new LoginViewModel();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is LoginViewModel vm)
            {
                vm.Username = UsernameBox.Text;
                vm.Password = PasswordBox.Password;
                vm.LoginCommand.Execute(null);
            }
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is LoginViewModel vm)
            {
                vm.Username = UsernameBox.Text;
                vm.Password = PasswordBox.Password;
                vm.RegisterCommand.Execute(null);
            }
        }
    }
}