﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepasoMio.Shared.DTO
{
    public class UserDTO : User
    {
        [Required(ErrorMessage = "El campo {0] es obligatorio")]
        [MaxLength(30, ErrorMessage = "El campo {0} debe tener maximo {1} caracter")]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "El campo {0] es obligatorio")]
        [MaxLength(30, ErrorMessage = "El campo {0} debe tener maximo {1} caracter")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Contraseñas no coinciden")]
        [Display(Name = "Confirmar contraseña")]
        public string? PasswordConfirm { get; set; }
    }
}
