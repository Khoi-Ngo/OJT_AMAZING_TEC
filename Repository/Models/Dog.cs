using System;
using System.Collections.Generic;

namespace Repository.Models;

public partial class Dog
{
    public int DogId { get; set; }

    public string? DogName { get; set; }

    public string? DogDescription { get; set; }

    public int? DogAge { get; set; }

    public int? DogTypeId { get; set; }

    public virtual DogType? DogType { get; set; }

    public virtual ICollection<Owning> Ownings { get; set; } = new List<Owning>();
}
