using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace qbu.Models;

[Table("Lecturer")]
public partial class Lecturer
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    public string Name { get; set; }

    [Required]
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
    public DateTime HireDate { get; set; }

    [InverseProperty("Lecturer")]
    public virtual ICollection<Course> Courses { get; } = new List<Course>();

    [ForeignKey("Id")]
    [InverseProperty("Lecturer")]
    public virtual Account IdNavigation { get; set; }
}
