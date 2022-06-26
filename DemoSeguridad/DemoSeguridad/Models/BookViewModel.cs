using DemoSeguridad.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DemoSeguridad.Models
{
    public class BookViewModel
    {
        public long Id { get; set; }
        [Display(Name = "Titulo")]
        public string Title { get; set; }
        [Display(Name = "Descripción")]
        public string Description { get; set; }
        [Display(Name = "Contenido")]
        [DataType(DataType.Text)]
        public string Content { get; set; }
        [Display(Name = "Autor")]
        public string AuthorName { get; set; }
    }
}