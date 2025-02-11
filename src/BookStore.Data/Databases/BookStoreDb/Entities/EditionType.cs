﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Data.Databases.BookStoreDb.Entities;

[Table("EDITION_TYPE")]
[Index("Code", Name = "UQ_EDITION_TYPE_CODE", IsUnique = true)]
public partial class EditionType
{
    [Key]
    [Column("EDITION_TYPE_ID")]
    public int EditionTypeId { get; set; }

    [Column("CODE")]
    [StringLength(5)]
    [Unicode(false)]
    public string Code { get; set; } = null!;

    [Column("NAME")]
    [StringLength(50)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [InverseProperty("EditionType")]
    public virtual ICollection<Edition> Edition { get; set; } = new List<Edition>();
}
