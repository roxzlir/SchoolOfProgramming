using System;
using System.Collections.Generic;

namespace SchoolOfProgramming.Models;

public partial class Profession
{
    public int ProfessionId { get; set; }

    public string? ProTitle { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
