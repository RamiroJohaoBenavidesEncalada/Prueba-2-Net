

using System.ComponentModel.DataAnnotations;
using Prueba.Biblioteca.Domain;

namespace Prueba.Biblioteca.Application;


public class LibroDto
{
    [Required]
    public int Id { get; set; }

    [Required]
    [StringLength(DominioConstantes.NOMBRE_MAXIMO)]
    public string Nombre { get; set; }

    public decimal Precio { get; set; }

    public string? Observaciones { get; set; }


    [Required]
    //[ForeignKey("Marca")]
    //EntidadId. Clave F. A la entidad 
    public int EditorialId { get; set; }

    public virtual Editorial Editorial { get; set; }


    [Required]
    public int AutorId { get; set; }

    public virtual Autor Autor { get; set; }
}

