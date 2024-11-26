using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionProductosJSON
{
    public class Producto
    {
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }

        public override string ToString()
        {
            return $"{Nombre} - Precio: {Precio:C} - Cantidad: {Cantidad}";
        }
    }

}
