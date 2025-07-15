using ExamenLenguajesVisuales1.Data;
using ExamenLenguajesVisuales1.Models;
using ExamenLenguajesVisuales1.Views;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace ExamenLenguajesVisuales1.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private string _usuario;
        private string _pass;
        private string _message;

        public string Username
        {
            get => _usuario;
            set { _usuario = value; OnPropertyChanged(); }
        }

        public string Password
        {
            get => _pass;
            set { _pass = value; OnPropertyChanged(); }
        }

        public string Message
        {
            get => _message;
            set { _message = value; OnPropertyChanged(); }
        }

        public ICommand LoginCommand { get; }
        public ICommand RegisterCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = new RelayCommand(Login);
            RegisterCommand = new RelayCommand(Register);
        }

        private void Login(object parameter)
        {
            using var db = new AppDbContext();
            var user = db.Users.FirstOrDefault(u => u.Nombre == Username && u.Password == Password);
            if (user != null)
            {
                MessageBox.Show("Login exitoso");
                var menu = new MenuPrincipalView();
                menu.Show();
                // Cierra la ventana de login si lo deseas:
                Application.Current.Windows[0]?.Close();
            }
            else
            {
                MessageBox.Show("Usuario o contraseña incorrectos");
            }
        }

        private void Register(object parameter)
        {
            var user = new User
            {
                Nombre = this.Username,

                Password = this.Password
            };

            using (var db = new AppDbContext())
            {
                db.Users.Add(user);
                db.SaveChanges();
            }

            MessageBox.Show("Usuario registrado correctamente");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
