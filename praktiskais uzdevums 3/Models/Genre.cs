using System;
using System.Collections.Generic;

namespace praktiskais_uzdevums_3.Models;

public partial class Genre
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
