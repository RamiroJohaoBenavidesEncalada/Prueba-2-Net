using System.ComponentModel.DataAnnotations;
using Prueba.Biblioteca.Domain;

namespace Prueba.Biblioteca.Application;

  
public class AutorCrearActualizarDto
{
 
    [Required]
    [StringLength(DominioConstantes.NOMBRE_MAXIMO)]
    public string Nombre {get;set;}

    public string Apellido {get;set;}
}