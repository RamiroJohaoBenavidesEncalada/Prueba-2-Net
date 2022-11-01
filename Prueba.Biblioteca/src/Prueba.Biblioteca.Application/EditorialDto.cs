

using System.ComponentModel.DataAnnotations;
using Prueba.Biblioteca.Domain;

namespace Prueba.Biblioteca.Application;


public class EditorialDto
{
    [Required]
    public int Id { get; set; }

    [Required]
    [StringLength(DominioConstantes.NOMBRE_MAXIMO)]
    public string Nombre { get; set; }
    public string? Direccion { get; set; }

    //Sigue en venta de libros
    public bool? Activa { get; set; }
}

