using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prueba.Biblioteca.Domain
{
    public class Libro
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(DominioConstantes.NOMBRE_MAXIMO)]
        public string Nombre { get; set; }

        public decimal Precio { get; set; }

        public string? Observaciones { get; set; }


        [Required]
        [ForeignKey("Editorial")]
        //EntidadId. Clave F. A la entidad 
        public int EditorialId { get; set; }

        public virtual Editorial Editorial { get; set; }


        [Required]
        [ForeignKey("Autor")]
        public int AutorId { get; set; }

        public virtual Autor Autor { get; set; }
    }
}