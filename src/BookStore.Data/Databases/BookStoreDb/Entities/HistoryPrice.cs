using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Data.Databases.BookStoreDb.Entities;

[PrimaryKey("EditionId", "Date")]
[Table("HISTORY_PRICE")]
public partial class HistoryPrice
{
    [Key]
    [Column("EDITION_ID")]
    public Guid EditionId { get; set; }

    [Key]
    [Column("DATE", TypeName = "datetime")]
    public DateTime Date { get; set; }

    [Column("PRICE", TypeName = "decimal(10, 2)")]
    public decimal Price { get; set; }

    [ForeignKey("EditionId")]
    [InverseProperty("HistoryPrice")]
    public virtual Edition Edition { get; set; } = null!;
}
