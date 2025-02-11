using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Data.Databases.BookStoreDb.Entities;

[Table("EDITION_PRICE")]
public partial class EditionPrice
{
    [Key]
    [Column("EDITION_ID")]
    public Guid EditionId { get; set; }

    [Column("PRICE", TypeName = "decimal(10, 2)")]
    public decimal Price { get; set; }

    [ForeignKey("EditionId")]
    [InverseProperty("EditionPrice")]
    public virtual Edition Edition { get; set; } = null!;
}
