using DemoSeguridad.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DemoSeguridad.Models
{
    public class BookCreateModel
    {
        [Display(Name = "T�tulo")]
        [Required(ErrorMessage = "Debe ingresar un t�tulo")]
        public string Title { get; set; }
        [Display(Name = "Descripci�n")]
        [Required(ErrorMessage = "Debe ingresar una descripci�n")]
        public string Description { get; set; }
        [Display(Name = "Contenido")]
        [Required(ErrorMessage = "Debe ingresar el contenido del libro")]
        public string Content { get; set; }
        [Display(Name = "Nombre del autor (Ya existente)")]
        [Required(ErrorMessage = "Debe ingresar el nombre del autor")]
        public string AuthorName { get; set; }
    }
}