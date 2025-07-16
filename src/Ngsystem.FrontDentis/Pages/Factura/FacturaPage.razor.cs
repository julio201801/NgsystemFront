using Microsoft.AspNetCore.Components;

public class FacturaPageBase : ComponentBase
{
    protected string ProductoBuscado { get; set; } = string.Empty;
    protected Producto ProductoSeleccionado { get; set; }


    protected Factura Factura { get; set; }

    protected override void OnInitialized()
    {
        // Inicializamos la factura con datos de ejemplo
        Factura = new Factura
        {
            Serie = "F001",
            Proveedor = "Proveedor X",
            Fecha = DateTime.Today,
            Detalles = new List<DetalleFactura>
                {
                    new() { Producto = "Talco para pies", Cantidad = 2, PrecioUnitario = 12, Marca = "PORTUGAL" },
                    new() { Producto = "Infusión de té",   Cantidad = 1, PrecioUnitario =  1, Marca = "HERBI"    },
                    new() { Producto = "Mata moscas",       Cantidad = 1, PrecioUnitario = 12, Marca = "SAPOLIO"  },
                }
        };
    }

    /// <summary>
    /// Método disparado al hacer click en "Buscar!"
    /// Aquí podrías llamar a tu API o filtrar la lista local.
    /// </summary>
    protected void BuscarProducto()
    {
        // Ejemplo: filtrar los detalles cuyo nombre contenga el texto de búsqueda
        Factura.Detalles = Factura.Detalles
            .Where(d => d.Producto.Contains(ProductoBuscado, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    /// <summary>
    /// Elimina un ítem del detalle de factura.
    /// </summary>
    protected void EliminarItem(DetalleFactura item)
    {
        Factura.Detalles.Remove(item);
    }

    //protected List<string> ProductosList = new()
    //{
    //    "Talco para pies",
    //    "Infusión de té",
    //    "Mata moscas",
    //    "Shampoo",
    //    "Jabón",
    //    "Jabón neko",
    //    "Jabón nivea",
    //    "Crema dental"
    //};
protected List<Producto> ProductosList = new()
{
  
        new Producto { Id =  1, Nombre = "Talco para pies",    Marca = "PORTUGAL", Precio = 12m  },
        new Producto { Id =  2, Nombre = "Infusión de té",      Marca = "HERBI",    Precio = 1m   },
        new Producto { Id =  3, Nombre = "Mata moscas",         Marca = "SAPOLIO",  Precio = 12m  },
        new Producto { Id =  4, Nombre = "Shampoo anticaspa",   Marca = "HEAD & SHOULDERS", Precio = 25.50m },
        new Producto { Id =  5, Nombre = "Jabón de tocador",     Marca = "DOVE",     Precio = 3.75m },
        new Producto { Id =  6, Nombre = "Crema dental",         Marca = "COLGATE",  Precio = 5.20m },
        new Producto { Id =  7, Nombre = "Detergente en polvo",  Marca = "ARIEL",    Precio = 18.99m },
        new Producto { Id =  8, Nombre = "Pasta de cocina",       Marca = "La Moderna", Precio = 4.30m },
        new Producto { Id =  9, Nombre = "Aceite vegetal",       Marca = "Cocinero", Precio = 10.00m },
        new Producto { Id = 10, Nombre = "Café molido",          Marca = "NESCAFÉ",  Precio = 15.00m }
    

};

    // Función que devuelve los ítems que coinciden con lo escrito
    //protected Task<IEnumerable<string>> BuscarProductos(string value)
    //{
    //    // Si no hay nada escrito, devolver todos
    //    if (string.IsNullOrWhiteSpace(value))
    //        return Task.FromResult(ProductosList.AsEnumerable());

    //    // Filtra ignorando mayúsculas/minúsculas
    //    var result = ProductosList
    //        .Where(x => x.Contains(value, StringComparison.OrdinalIgnoreCase));
    //    return Task.FromResult(result);
    //}
    protected Task<IEnumerable<Producto>> BuscarProductos(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return Task.FromResult(ProductosList.AsEnumerable());

        var result = ProductosList
            .Where(p => p.Nombre.Contains(text, StringComparison.OrdinalIgnoreCase));
        return Task.FromResult(result);
    }

    protected void OnProductoSeleccionado(Producto p)
    {
        if (p == null)
            return;

        // Crear la línea de detalle con Cantidad=1 por defecto
        Factura.Detalles.Add(new DetalleFactura
        {
            Producto = p.Nombre,
            Marca = p.Marca,
            Cantidad = 1,
            PrecioUnitario = p.Precio
        });

        // Limpiar selección para seguir agregando nuevos productos
        ProductoSeleccionado = null;
    }


}

/// <summary>
/// Modelo de la factura (cabecera).
/// </summary>
/// <summary>
/// Modelo de la factura (cabecera).
/// </summary>
public class Factura
{
    public string Serie { get; set; }
    public string Proveedor { get; set; }
    public DateTime? Fecha { get; set; }   // Nullable para compatibilidad con MudDatePicker
    public List<DetalleFactura> Detalles { get; set; } = new();

    public decimal SubTotal => Detalles.Sum(x => x.Subtotal);
    public decimal IGV => Math.Round(SubTotal * 0.18M, 2);
    public decimal Total => SubTotal + IGV;

}

/// <summary>
/// Modelo de cada línea de detalle en la factura.
/// </summary>
public class DetalleFactura
{
    public string Producto { get; set; }
    public string Marca { get; set; }
    public int Cantidad { get; set; }
    public decimal PrecioUnitario { get; set; }
    public decimal Subtotal => Cantidad * PrecioUnitario;
}
public class Producto
{
    /// <summary>
    /// Identificador único (si lo obtienes de BD o API).
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Nombre descriptivo del producto.
    /// </summary>
    public string Nombre { get; set; } = string.Empty;

    /// <summary>
    /// Marca o fabricante.
    /// </summary>
    public string Marca { get; set; } = string.Empty;

    /// <summary>
    /// Precio unitario (sin impuestos).
    /// </summary>
    public decimal Precio { get; set; }

    /// <summary>
    /// Si usas EF Core y quieres mapear stock.
    /// </summary>
    // public int Stock { get; set; }

    public override string ToString()
        => Nombre;  // Facilita que el autocomplete muestre el Nombre
}
