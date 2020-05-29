using System;
using System.Collections.Generic;

namespace testwebapicore.Models
{
    public partial class Role
    {
        public Role()
        {
            User = new HashSet<User>();
        }

        public int Id { get; set; }
        public string Role1 { get; set; }

        public virtual ICollection<User> User { get; set; }
    }
}
