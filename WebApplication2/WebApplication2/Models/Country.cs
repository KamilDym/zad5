using System;
using System.Collections.Generic;

namespace WebApplication2.Models;

public partial class Country
{
    public int IdCountry { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Trips> IdTrips { get; set; } = new List<Trips>();
}
