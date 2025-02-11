using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Data.Databases.BookStoreDb.Entities;

[Table("GENRE")]
[Index("Code", Name = "UQ_GENRE_CODE", IsUnique = true)]
public partial class Genre
{
    [Key]
    [Column("GENRE_ID")]
    public int GenreId { get; set; }

    [Column("CODE")]
    [StringLength(5)]
    [Unicode(false)]
    public string Code { get; set; } = null!;

    [Column("NAME")]
    [StringLength(50)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [ForeignKey("GenreId")]
    [InverseProperty("Genre")]
    public virtual ICollection<Book> Book { get; set; } = new List<Book>();
}
