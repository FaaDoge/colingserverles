using Azure;
using Azure.Data.Tables;
using ColingShared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coling.Vista.Modelos
{
    public class Estudios : IEstudios
    {
        [Display(Name = "IdAfiliado")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int IdAfiliado { get; set; }
        [Display(Name = "TipoEstudio")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(100, ErrorMessage = "El campo {0} de tener maximo {1} caracteres")]
        public string TipoEstudio { get; set; }
        [Display(Name = "NombreGrado")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(100, ErrorMessage = "El campo {0} de tener maximo {1} caracteres")]
        public string NombreGrado { get; set; }
        [Display(Name = "TituloRcibido")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(100, ErrorMessage = "El campo {0} de tener maximo {1} caracteres")]
        public string TituloRecibido { get; set; }
        [Display(Name = "NombreInstitucion")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(100, ErrorMessage = "El campo {0} de tener maximo {1} caracteres")]
        public string NombreInstitucion { get; set; }
        [Range(1800, 3999, ErrorMessage = "Por favor, ingresa un año válido.")]
        public int Anio { get; set; }
        public string Estado { get; set; }
        //variables ITable
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public string ETag { get; set; }

    }
}
