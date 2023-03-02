using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace qbu.Models;

[Keyless]
[Table("Grade")]
[Index("CourseId", "StudentId", Name = "IX_Grade", IsUnique = true)]
public partial class Grade
{
    [Column("CourseID")]
    public int? CourseId { get; set; }

    [Column("StudentID")]
    public int? StudentId { get; set; }

    [Column("Grade")]
    [StringLength(2)]
    public string Grade1 { get; set; }

    [ForeignKey("CourseId")]
    public virtual Course Course { get; set; }

    [ForeignKey("StudentId")]
    public virtual Student Student { get; set; }
}
