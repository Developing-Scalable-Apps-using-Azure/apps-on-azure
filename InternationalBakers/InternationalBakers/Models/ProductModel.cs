﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace InternationalBakers.Models;

[Table("ProductModel", Schema = "SalesLT")]
[Index("Name", Name = "AK_ProductModel_Name", IsUnique = true)]
[Index("Rowguid", Name = "AK_ProductModel_rowguid", IsUnique = true)]
public partial class ProductModel
{
    [Key]
    [Column("ProductModelID")]
    public int ProductModelId { get; set; }

    [StringLength(50)]
    public string Name { get; set; } = null!;

    [Column(TypeName = "xml")]
    public string? CatalogDescription { get; set; }

    [Column("rowguid")]
    public Guid Rowguid { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime ModifiedDate { get; set; }

    [InverseProperty("ProductModel")]
    public virtual ICollection<ProductModelProductDescription> ProductModelProductDescriptions { get; set; } = new List<ProductModelProductDescription>();

    [InverseProperty("ProductModel")]
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
