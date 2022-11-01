using System.ComponentModel.DataAnnotations;
using Prueba.Biblioteca.Domain;

namespace Prueba.Biblioteca.Application;

  
public class LibroCrearActualizarDto
{
 
    [Required]
    [StringLength(DominioConstantes.NOMBRE_MAXIMO)]
    public string Nombre { get; set; }

    public decimal Precio { get; set; }

    public string? Observaciones { get; set; }


    [Required]
    //[ForeignKey("Marca")]
    //EntidadId. Clave F. A la entidad 
    public int EditorialId { get; set; }
    

    [Required]
    public int AutorId { get; set; }

    
}