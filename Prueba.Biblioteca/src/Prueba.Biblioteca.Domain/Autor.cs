using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Prueba.Biblioteca.Domain
{
    public class Autor
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
}