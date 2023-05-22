using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace IPC.Correios.Application.ViewModels
{
    public class BuscaEnderecoViewModel
    {
        [Required]
        public string Estado { get; set; }

        [Required]
        [Display(Description = "Município")]
        public string Municipio { get; set; }

        [Required]
        public string Logradouro { get; set; }
    }
}
