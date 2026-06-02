using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Pos.Models;

/// <summary>
/// CREATE TABLE [dbo].[Packages] (
///     [Id]               INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
///     [DeliveryId]       INT NOT NULL,                         -- Links to Deliveries table
///     [TrackingNumber]   NVARCHAR(100) NOT NULL UNIQUE,        -- Unique tracking number for the package
///     [Weight]           DECIMAL(10, 2) NULL,                  -- Weight of the package in kilograms or pounds
///     [Dimensions]       NVARCHAR(100) NULL,                   -- Dimensions (e.g., &quot;30x20x10 cm&quot;)
///     [Description]      NVARCHAR(255) NULL,                   -- Short description of the package content
///     [Status]           NVARCHAR(50) NOT NULL,                -- Status (e.g., &quot;Packed&quot;, &quot;In Transit&quot;, &quot;Delivered&quot;)
///     [CreatedAt]        DATETIME2(7) NOT NULL DEFAULT SYSDATETIME(),
///     [UpdatedAt]        DATETIME2(7) NOT NULL DEFAULT SYSDATETIME(),
///     CONSTRAINT FK_Packages_DeliveryId FOREIGN KEY ([DeliveryId]) REFERENCES [dbo].[Deliveries]([Id])
/// );
/// 
/// </summary>
[Index("TrackingNumber", Name = "UQ__Packages__784DB3D9C1B2EE99", IsUnique = true)]
public partial class Package
{
    [Key]
    public int Id { get; set; }

    public int DeliveryId { get; set; }

    [Required]
    [StringLength(100)]
    public string TrackingNumber { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? Weight { get; set; }

    [StringLength(100)]
    public string Dimensions { get; set; }

    [StringLength(255)]
    public string Description { get; set; }

    [Required]
    [StringLength(50)]
    public string Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
