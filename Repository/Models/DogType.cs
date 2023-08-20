using System;
using System.Collections.Generic;

namespace Repository.Models;

public partial class DogType
{
    public int Id { get; set; }

    public string? DogTypeName { get; set; }

    public virtual ICollection<Dog> Dogs { get; set; } = new List<Dog>();
}
