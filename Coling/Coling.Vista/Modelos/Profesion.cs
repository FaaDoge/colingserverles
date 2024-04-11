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
    public class Profesion : IProfesion
    {
        [Display(Name = "NombreProfesion")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(100, ErrorMessage = "El campo {0} de tener maximo {1} caracteres")]
        public string NombreProfesion { get; set; }
        [Display(Name = "NombreGrado")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MaxLength(100, ErrorMessage = "El campo {0} de tener maximo {1} caracteres")]
        public string NombreGrado { get; set; }
        public string Estado { get; set; }
        //table
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public string ETag { get; set; }
    }
}
