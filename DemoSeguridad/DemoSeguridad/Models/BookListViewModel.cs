using System;
using System.Collections.Generic;

namespace DemoSeguridad.Models
{
    public class BookListViewModel
    {
        public BookListViewModel()
        {
            Books = new List<BookViewModel>();
        }
        public ICollection<BookViewModel> Books { get; set; }
    }
}