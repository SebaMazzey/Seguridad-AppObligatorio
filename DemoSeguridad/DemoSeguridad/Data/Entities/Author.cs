using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DemoSeguridad.Data.Entities
{
    public class Author
    {
        public Author()
        {
            Books = new HashSet<Book>();
        }

        [Key]
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<Book> Books { get; set; }
    }
}