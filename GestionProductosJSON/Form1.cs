using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GestionProductosJSON
{
    public partial class Form1 : Form
    {
        private List<Producto> listaProductos = new List<Producto>();

        public Form1()
        {
            InitializeComponent();
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
           

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                // Crear un nuevo producto a partir de los datos ingresados
                Producto nuevoProducto = new Producto
                {
                    Nombre = txtNombre.Text,
                    Precio = decimal.Parse(txtPrecio.Text),
                    Cantidad = int.Parse(txtCantidad.Text)
                };

                listaProductos.Add(nuevoProducto); // Agregar el producto a la lista
                lstProductos.Items.Add(nuevoProducto); // Mostrarlo en el ListBox

                // Limpiar los campos
                txtNombre.Clear();
                txtPrecio.Clear();
                txtCantidad.Clear();
                MessageBox.Show("Producto agregado correctamente.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar producto: " + ex.Message);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                // Verificar si hay productos en la lista
                if (listaProductos.Count == 0)
                {
                    MessageBox.Show("No hay productos para guardar.");
                    return;
                }

                // Convertir la lista de productos a formato JSON
                string json = JsonSerializer.Serialize(listaProductos, new JsonSerializerOptions { WriteIndented = true });

                // Guardar el archivo JSON en el escritorio
                string rutaArchivo = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "productos.json");
                File.WriteAllText(rutaArchivo, json);

                MessageBox.Show($"Datos guardados correctamente en: {rutaArchivo}");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar en JSON: " + ex.Message);
            }
        }
        private void btnCargar_Click(object sender, EventArgs e)
        {
            try
            {
                // Definir la ruta del archivo JSON
                string rutaArchivo = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "productos.json");

                // Verificar si el archivo existe
                if (!File.Exists(rutaArchivo))
                {
                    MessageBox.Show("El archivo productos.json no existe en el escritorio.");
                    return;
                }

                // Leer el contenido del archivo JSON
                string json = File.ReadAllText(rutaArchivo);

                // Deserializar el JSON a la lista de productos
                listaProductos = JsonSerializer.Deserialize<List<Producto>>(json);

                // Actualizar el ListBox con los productos cargados
                lstProductos.Items.Clear();
                foreach (var producto in listaProductos)
                {
                    lstProductos.Items.Add(producto);
                }

                MessageBox.Show("Datos cargados correctamente desde el archivo JSON.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar desde JSON: " + ex.Message);
            }
        }
        private void btnSalir_Click(object sender, EventArgs e)
        {
            // Confirmar si el usuario desea cerrar la aplicación
            DialogResult resultado = MessageBox.Show("¿Estás seguro de que deseas salir?", "Confirmar salida", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                Application.Exit(); // Cerrar la aplicación
            }
        }
    }
}
