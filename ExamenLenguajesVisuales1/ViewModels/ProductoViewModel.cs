using ExamenLenguajesVisuales1.Data;
using ExamenLenguajesVisuales1.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace ExamenLenguajesVisuales1.ViewModels
{
    public class ProductoViewModel : INotifyPropertyChanged
    {
        private string _nombre = string.Empty;
        private string? _descripcion = string.Empty;
        private decimal _precio = 0;
        private int _stock = 0;
        private int _categoriaId = 0;
        private Producto? _productoSeleccionado;

        public ObservableCollection<Producto> Productos { get; } = new();
        public ObservableCollection<Categoria> Categorias { get; } = new();

        public string Nombre
        {
            get => _nombre;
            set { _nombre = value; OnPropertyChanged(); }
        }
        public string? Descripcion
        {
            get => _descripcion;
            set { _descripcion = value; OnPropertyChanged(); }
        }
        public decimal Precio
        {
            get => _precio;
            set { _precio = value; OnPropertyChanged(); }
        }
        public int Stock
        {
            get => _stock;
            set { _stock = value; OnPropertyChanged(); }
        }
        public int CategoriaId
        {
            get => _categoriaId;
            set { _categoriaId = value; OnPropertyChanged(); }
        }
        public Producto? ProductoSeleccionado
        {
            get => _productoSeleccionado;
            set
            {
                _productoSeleccionado = value;
                if (value != null)
                {
                    Nombre = value.Nombre ?? string.Empty;
                    Descripcion = value.Descripcion ?? string.Empty;
                    Precio = value.Precio;
                    Stock = value.Stock;
                    CategoriaId = value.CategoriaId;
                }
                else
                {
                    LimpiarCampos();
                }
                OnPropertyChanged();
            }
        }
        public int Id => ProductoSeleccionado?.Id ?? 0;

        public ICommand AgregarCommand { get; }
        public ICommand EditarCommand { get; }
        public ICommand EliminarCommand { get; }

        public ProductoViewModel()
        {
            try
            {
                AgregarCommand = new RelayCommand(AgregarProducto);
                EditarCommand = new RelayCommand(EditarProducto, (obj) => ProductoSeleccionado != null);
                EliminarCommand = new RelayCommand(EliminarProducto, (obj) => ProductoSeleccionado != null);
                CargarCategorias();
                CargarProductos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en ProductoViewModel: " + ex.Message + 
                                (ex.InnerException != null ? "\n" + ex.InnerException.Message : ""));
                System.Diagnostics.Debug.WriteLine("Error en ProductoViewModel: " + ex);
            }
        }

        private void CargarCategorias()
        {
            Categorias.Clear();
            using var db = new AppDbContext();
            foreach (var cat in db.Categorias)
                Categorias.Add(cat);
        }

        private void CargarProductos()
        {
            Productos.Clear();
            using var db = new AppDbContext();
            foreach (var prod in db.Productos)
                Productos.Add(prod);
        }

        private bool ValidarProducto(out string mensaje)
        {
            if (string.IsNullOrWhiteSpace(Nombre))
            {
                mensaje = "El nombre es obligatorio.";
                return false;
            }
            if (Precio <= 0)
            {
                mensaje = "El precio debe ser mayor a 0.";
                return false;
            }
            if (Stock < 0)
            {
                mensaje = "El stock no puede ser negativo.";
                return false;
            }
            if (CategoriaId == 0)
            {
                mensaje = "Debes seleccionar una categoría.";
                return false;
            }
            using var db = new AppDbContext();
            if (!db.Categorias.Any(c => c.Id == CategoriaId))
            {
                mensaje = "La categoría seleccionada no existe.";
                return false;
            }
            mensaje = string.Empty;
            return true;
        }

        private void AgregarProducto(object? parameter)
        {
            if (!ValidarProducto(out string mensaje))
            {
                MessageBox.Show(mensaje);
                return;
            }

            var nuevo = new Producto
            {
                // No asignes 'Id' aquí
                Nombre = Nombre,
                Descripcion = string.IsNullOrWhiteSpace(Descripcion) ? null : Descripcion,
                Precio = Precio,
                Stock = Stock,
                CategoriaId = CategoriaId,
                UsuarioId = 1 // Ajusta según el usuario logueado
            };

            using var db = new AppDbContext();
            db.Productos.Add(nuevo);
            try
            {
                db.SaveChanges();
                Productos.Add(nuevo); // Solo agrega el nuevo producto, no recarga toda la lista
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar: " + (ex.InnerException?.Message ?? ex.Message));
            }
        }

        private void EditarProducto(object? parameter)
        {
            if (ProductoSeleccionado == null)
            {
                MessageBox.Show("Selecciona un producto para editar.");
                return;
            }
            if (!ValidarProducto(out string mensaje))
            {
                MessageBox.Show(mensaje);
                return;
            }

            using var db = new AppDbContext();
            var prod = db.Productos.Find(ProductoSeleccionado.Id);
            if (prod != null)
            {
                prod.Nombre = Nombre;
                prod.Descripcion = string.IsNullOrWhiteSpace(Descripcion) ? null : Descripcion;
                prod.Precio = Precio;
                prod.Stock = Stock;
                prod.CategoriaId = CategoriaId;
                try
                {
                    db.SaveChanges();
                    // Actualiza el producto en la lista sin recargar todo
                    var index = Productos.IndexOf(ProductoSeleccionado);
                    if (index >= 0)
                    {
                        Productos[index] = prod;
                    }
                    LimpiarCampos();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al editar: " + (ex.InnerException?.Message ?? ex.Message));
                }
            }
        }

        private void EliminarProducto(object? parameter)
        {
            if (ProductoSeleccionado == null)
            {
                MessageBox.Show("Selecciona un producto para eliminar.");
                return;
            }

            using var db = new AppDbContext();
            var prod = db.Productos.Find(ProductoSeleccionado.Id);
            if (prod != null)
            {
                db.Productos.Remove(prod);
                try
                {
                    db.SaveChanges();
                    Productos.Remove(ProductoSeleccionado); // Solo elimina el producto de la lista
                    LimpiarCampos();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar: " + (ex.InnerException?.Message ?? ex.Message));
                }
            }
        }

        private void LimpiarCampos()
        {
            Nombre = string.Empty;
            Descripcion = string.Empty;
            Precio = 0;
            Stock = 0;
            CategoriaId = 0;
            ProductoSeleccionado = null;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
