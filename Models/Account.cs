using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace qbu.Models;

[Table("Account")]
[Index("Email", Name = "IX_Account", IsUnique = true)]
public partial class Account
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    public string Email { get; set; }

    [Required]
    [StringLength(20)]
    [Unicode(false)]
    public string Password { get; set; }

    [InverseProperty("IdNavigation")]
    public virtual Lecturer Lecturer { get; set; }

    [InverseProperty("IdNavigation")]
    public virtual Student Student { get; set; }
}
