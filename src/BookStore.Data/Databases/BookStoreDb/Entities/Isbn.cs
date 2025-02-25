using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Data.Databases.BookStoreDb.Entities;

[Table("ISBN")]
[Index("Code", Name = "UQ_ISBN_CODE", IsUnique = true)]
public partial class Isbn
{
    [Key]
    [Column("EDITION_ID")]
    public Guid EditionId { get; set; }

    [Column("CODE")]
    [StringLength(13)]
    [Unicode(false)]
    public string Code { get; set; } = null!;

    [ForeignKey("EditionId")]
    [InverseProperty("Isbn")]
    public virtual Edition Edition { get; set; } = null!;
}
