using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KB.BookStore.WebApi.Models.Entities
{
    [Table("Categories")]
    public class Category
    {
        [Key] public Guid Id { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<Book>? Books { get; set; } = new List<Book>();
    }
}
