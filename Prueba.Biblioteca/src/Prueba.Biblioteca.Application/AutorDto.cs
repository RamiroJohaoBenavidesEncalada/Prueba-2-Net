

using System.ComponentModel.DataAnnotations;
using Prueba.Biblioteca.Domain;

namespace Prueba.Biblioteca.Application;

 
public class AutorDto
{
    [Required]
        [StringLength(DominioConstantes.NOMBRE_MAXIMO)]
        public int Id {get;set;}
        [Required]
        [StringLength(DominioConstantes.NOMBRE_MAXIMO)]
        public string Nombre {get;set;}
        [Required]
        [StringLength(DominioConstantes.NOMBRE_MAXIMO)]
        public string Apellido {get;set;}
        public bool Relevante{get;set;}
}

 