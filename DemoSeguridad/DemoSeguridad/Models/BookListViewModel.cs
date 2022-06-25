using DemoSeguridad.Data.Entities;
using System;
using System.Collections.Generic;

namespace DemoSeguridad.Models
{
    public class BookListViewModel
    {
        public ICollection<Book> Books { get; set; }
    }
}