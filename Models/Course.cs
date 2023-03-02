using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace qbu.Models;

[Table("Course")]
public partial class Course
{
    [Key]
    [Column("CourseID")]
    public int CourseId { get; set; }

    [Column("LecturerID")]
    public int? LecturerId { get; set; }

    [StringLength(-1)]
    public string Title { get; set; }

    [ForeignKey("LecturerId")]
    [InverseProperty("Courses")]
    public virtual Lecturer Lecturer { get; set; }
}
