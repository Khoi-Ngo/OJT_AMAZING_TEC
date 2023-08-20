using System;
using System.Collections.Generic;

namespace Repository.Models;

public partial class Owner
{
    public int OwnerId { get; set; }

    public string? OwnerName { get; set; }

    public int? OwnerAge { get; set; }

    public virtual ICollection<Owning> Ownings { get; set; } = new List<Owning>();
}
