using System;
using System.Collections.Generic;

namespace testwebapicore.Models
{
    public partial class Waste
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal? Price { get; set; }
        public decimal? Percent { get; set; }
    }
}
