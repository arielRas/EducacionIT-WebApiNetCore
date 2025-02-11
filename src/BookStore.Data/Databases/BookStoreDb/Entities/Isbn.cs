using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Data.Databases.BookStoreDb.Entities;

[Table("ISBN")]
[Index("EditionId", Name = "UQ_ISBN_EDITION_ID", IsUnique = true)]
public partial class Isbn
{
    [Key]
    [Column("CODE")]
    [StringLength(13)]
    [Unicode(false)]
    public string Code { get; set; } = null!;

    [Column("EDITION_ID")]
    public Guid EditionId { get; set; }

    [ForeignKey("EditionId")]
    [InverseProperty("Isbn")]
    public virtual Edition Edition { get; set; } = null!;
}
