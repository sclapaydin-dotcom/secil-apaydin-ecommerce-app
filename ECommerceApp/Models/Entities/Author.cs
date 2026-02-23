using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KB.BookStore.WebApi.Models.Entities
{
    [Table("Authors")]
    public class Author
    {
        [Key] public Guid Id { get; set; }

        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public DateTime? BirthDate { get; set; }

        public virtual ICollection<Book>? Books { get; set; } = new List<Book>();
    }
}
