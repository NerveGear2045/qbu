using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace qbu.Models;

[Table("Student")]
[Index("Id", Name = "IX_Student", IsUnique = true)]
public partial class Student
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    public string Name { get; set; }

    [StringLength(11)]
    [Unicode(false)]
    public string Phone { get; set; }
    [DataType(DataType.Date)]
    public DateTime Birth { get; set; }

    [Required]
    [StringLength(50)]
    public string Sex { get; set; }

    [StringLength(50)]
    public string Address { get; set; }

    [StringLength(50)]
    public string District { get; set; }

    [StringLength(50)]
    public string City { get; set; }

    [Required]
    [StringLength(50)]
    public string Major { get; set; }
    [DataType(DataType.Date)]

    public DateTime EnrollmentDate { get; set; }

    public bool Undergrad { get; set; }

    [ForeignKey("Id")]
    [InverseProperty("Student")]
    public virtual Account IdNavigation { get; set; }
}
