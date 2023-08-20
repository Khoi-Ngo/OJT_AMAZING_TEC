using System;
using System.Collections.Generic;

namespace Repository.Models;

public partial class Owning
{
    public int Id { get; set; }

    public int? OwnerId { get; set; }

    public int? DogId { get; set; }

    public virtual Dog? Dog { get; set; }

    public virtual Owner? Owner { get; set; }
}
