using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DemoSeguridad.Data.Entities
{
    public class Book
    {
        [Key]
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public long AuthorId { get; set; }
        public Author Author { get; set; }
    }
}