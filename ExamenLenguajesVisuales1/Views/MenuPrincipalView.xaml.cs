using System.Windows;

namespace ExamenLenguajesVisuales1.Views
{
    public partial class MenuPrincipalView : Window
    {
        public MenuPrincipalView()
        {
            InitializeComponent();
        }

        private void Categorias_Click(object sender, RoutedEventArgs e)
        {
            var win = new CategoriaView();
            win.ShowDialog();
        }

         private void Productos_Click(object sender, RoutedEventArgs e)
          {
            var win = new ProductoView();
             win.ShowDialog();
         }
    }
}
