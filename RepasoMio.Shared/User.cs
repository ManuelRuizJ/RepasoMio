﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace RepasoMio.Shared
{
    public class User:IdentityUser
    {
        [Required(ErrorMessage = "El campo {0] es obligatorio")]
        [MaxLength(100, ErrorMessage = "El campo {0} debe tener maximo {1} caracter")]
        [Display(Name = "Nombre(s)")]
        public string FirstName { get; set; }= null!;

        [Required(ErrorMessage = "El campo {0] es obligatorio")]
        [MaxLength(100, ErrorMessage = "El campo {0} debe tener maximo {1} caracter")]
        [Display(Name = "Apellido(s)")]
        public string LastName { get; set; } = null!;

        [Display(Name = "Photo")]
        [DataType(DataType.ImageUrl)]
        public string? Photo { get; set; }

        public UserType UserType { get; set; }

        [Display(Name = "Ciudad")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes seleccionar {0}")]
        public int CityId { get; set; }

        public City? City { get; set; }

        public string FullName => $"{this.FirstName} {this.LastName}";
    }
}
