using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Data.Databases.BookStoreDb.Entities;

[Table("AUTHOR")]
public partial class Author
{
    [Key]
    [Column("AUTHOR_ID")]
    public int AuthorId { get; set; }

    [Column("NAME")]
    [StringLength(50)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [Column("LAST_NAME")]
    [StringLength(50)]
    [Unicode(false)]
    public string? LastName { get; set; }

    [ForeignKey("AuthorId")]
    [InverseProperty("Author")]
    public virtual ICollection<Book> Book { get; set; } = new List<Book>();
}
