using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KB.BookStore.WebApi.Models.Entities
{
    [Table("Books")]
    public class Book
    {
        [Key] public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public short? PageCount { get; set; }
        public DateTime? ReleaseDate { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        public int? Quantity { get; set; }

        public Guid AuthorId { get; set; }
        [ForeignKey(nameof(AuthorId))]
        public virtual Author? Author { get; set; } = new Author();

        public Guid CategoryId { get; set; }
        [ForeignKey(nameof(CategoryId))]
        public virtual Category? Category { get; set; } = new Category();
    }
}
