using System;
using System.Collections.Generic;

namespace testwebapicore.Models
{
    public partial class ComapnyPromotion
    {
        public ComapnyPromotion()
        {
            Promotions = new HashSet<Promotions>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Promotions> Promotions { get; set; }
    }
}
