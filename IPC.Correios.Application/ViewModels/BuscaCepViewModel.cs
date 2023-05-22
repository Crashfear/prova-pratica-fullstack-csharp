using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace IPC.Correios.Application.ViewModels
{
    public class BuscaCepViewModel
    {
        [Range(0, 9, ErrorMessage = "O cep deve ser composto apenas por números")]
        [Required]
        [StringLength(8, ErrorMessage = "O cep não pode ter mais que 8 digitos")]
        public string Cep { get; set; }
    }
}
