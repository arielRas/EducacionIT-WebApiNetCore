using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Data.Databases.BookStoreDb.Entities;

[Table("BOOK")]
public partial class Book
{
    [Key]
    [Column("BOOK_ID")]
    public int BookId { get; set; }

    [Column("TITLE")]
    [StringLength(200)]
    [Unicode(false)]
    public string Title { get; set; } = null!;

    [Column("SYNOPSIS")]
    [Unicode(false)]
    public string? Synopsis { get; set; }

    [InverseProperty("Book")]
    public virtual ICollection<Edition> Editions { get; set; } = new List<Edition>();

    [ForeignKey("BookId")]
    [InverseProperty("Books")]
    public virtual ICollection<Author> Authors { get; set; } = new List<Author>();

    [ForeignKey("BookId")]
    [InverseProperty("Books")]
    public virtual ICollection<Genre> Genres { get; set; } = new List<Genre>();
}
