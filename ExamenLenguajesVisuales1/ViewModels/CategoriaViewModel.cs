using ExamenLenguajesVisuales1.Data;
using ExamenLenguajesVisuales1.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace ExamenLenguajesVisuales1.ViewModels
{
    public class CategoriaViewModel : INotifyPropertyChanged
    {
        private string _nombre;
        private Categoria _categoriaSeleccionada;

        public ObservableCollection<Categoria> Categorias { get; set; } = new();

        public string Nombre
        {
            get => _nombre;
            set { _nombre = value; OnPropertyChanged(); }
        }

        public Categoria CategoriaSeleccionada
        {
            get => _categoriaSeleccionada;
            set { _categoriaSeleccionada = value; OnPropertyChanged(); }
        }

        public ICommand AgregarCommand { get; }
        public ICommand EditarCommand { get; }
        public ICommand EliminarCommand { get; }

        public CategoriaViewModel()
        {
            AgregarCommand = new RelayCommand(AgregarCategoria);
            EditarCommand = new RelayCommand(EditarCategoria, (obj) => CategoriaSeleccionada != null);
            EliminarCommand = new RelayCommand(EliminarCategoria, (obj) => CategoriaSeleccionada != null);
            CargarCategorias();
        }

        private void CargarCategorias()
        {
            using var db = new AppDbContext();
            Categorias.Clear();
            foreach (var cat in db.Categorias.ToList())
                Categorias.Add(cat);
        }

        private void AgregarCategoria(object parameter)
        {
            if (string.IsNullOrWhiteSpace(Nombre))
            {
                MessageBox.Show("El nombre es obligatorio.");
                return;
            }

            using var db = new AppDbContext();
            if (db.Categorias.Any(c => c.Nombre == Nombre))
            {
                MessageBox.Show("Ya existe una categoría con ese nombre.");
                return;
            }

            var nueva = new Categoria { Nombre = Nombre };
            db.Categorias.Add(nueva);
            db.SaveChanges();
            CargarCategorias();
            Nombre = string.Empty;
        }

        private void EditarCategoria(object parameter)
        {
            if (CategoriaSeleccionada == null) return;
            if (string.IsNullOrWhiteSpace(Nombre))
            {
                MessageBox.Show("El nombre es obligatorio.");
                return;
            }

            using var db = new AppDbContext();
            var cat = db.Categorias.Find(CategoriaSeleccionada.Id);
            if (cat != null)
            {
                cat.Nombre = Nombre;
                db.SaveChanges();
                CargarCategorias();
                Nombre = string.Empty;
            }
        }

        private void EliminarCategoria(object parameter)
        {
            if (CategoriaSeleccionada == null) return;

            using var db = new AppDbContext();
            var cat = db.Categorias.Find(CategoriaSeleccionada.Id);
            if (cat != null)
            {
                db.Categorias.Remove(cat);
                db.SaveChanges();
                CargarCategorias();
                Nombre = string.Empty;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
