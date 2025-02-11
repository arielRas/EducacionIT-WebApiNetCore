using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Data.Databases.BookStoreDb.Entities;

[Table("EDITION_STOCK")]
public partial class EditionStock
{
    [Key]
    [Column("EDITION_ID")]
    public Guid EditionId { get; set; }

    [Column("STOCK")]
    public int Stock { get; set; }

    [ForeignKey("EditionId")]
    [InverseProperty("EditionStock")]
    public virtual Edition Edition { get; set; } = null!;
}
