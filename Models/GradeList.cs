using System;
using System.Collections.Generic;

namespace SchoolOfProgramming.Models;

public partial class GradeList
{
    public int? FkGradeId { get; set; }

    public DateTime? GradeDate { get; set; }

    public int? FkEnrollmentId { get; set; }

    public virtual EnrollmentList? FkEnrollment { get; set; }

    public virtual Grade? FkGrade { get; set; }
}
