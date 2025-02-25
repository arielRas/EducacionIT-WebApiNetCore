using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Data.Databases.BookStoreDb.Entities;

[Table("EDITORIAL")]
public partial class Editorial
{
    [Key]
    [Column("EDITORIAL_ID")]
    public int EditorialId { get; set; }

    [Column("NAME")]
    [StringLength(50)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [InverseProperty("Editorial")]
    public virtual ICollection<Edition> Edition { get; set; } = new List<Edition>();
}
