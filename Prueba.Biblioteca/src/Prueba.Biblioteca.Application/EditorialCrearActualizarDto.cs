using System.ComponentModel.DataAnnotations;
using Prueba.Biblioteca.Domain;

namespace Prueba.Biblioteca.Application;

  
public class EditorialCrearActualizarDto
{
 
    [Required]
    [StringLength(DominioConstantes.NOMBRE_MAXIMO)]
    public string Nombre {get;set;}
}