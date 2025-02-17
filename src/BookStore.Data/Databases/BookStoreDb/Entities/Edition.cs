using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Data.Databases.BookStoreDb.Entities;

[Table("EDITION")]
public partial class Edition
{
    [Key]
    [Column("EDITION_ID")]
    public Guid EditionId { get; set; }

    [Column("PAGES")]
    public int Pages { get; set; }

    [Column("PUBLICATION_DATE")]
    public DateOnly PublicationDate { get; set; }

    [Column("LANGUAGE")]
    [StringLength(50)]
    [Unicode(false)]
    public string Language { get; set; } = null!;

    [Column("BOOK_ID")]
    public int BookId { get; set; }

    [Column("TYPE_CODE")]
    [StringLength(5)]
    [Unicode(false)]
    public string TypeCode { get; set; } = null!;

    [Column("EDITORIAL_ID")]
    public int EditorialId { get; set; }

    [ForeignKey("BookId")]
    [InverseProperty("Edition")]
    public virtual Book Book { get; set; } = null!;

    [InverseProperty("Edition")]
    public virtual EditionPrice? EditionPrice { get; set; }

    [ForeignKey("EditorialId")]
    [InverseProperty("Edition")]
    public virtual Editorial Editorial { get; set; } = null!;

    [InverseProperty("Edition")]
    public virtual ICollection<HistoryPrice> HistoryPrice { get; set; } = new List<HistoryPrice>();

    [InverseProperty("Edition")]
    public virtual Isbn? Isbn { get; set; }

    [ForeignKey("TypeCode")]
    [InverseProperty("Edition")]
    public virtual EditionType TypeCodeNavigation { get; set; } = null!;
}
