using System;
using System.Collections.Generic;

namespace testwebapicore.Models
{
    public partial class ClientCategory
    {
        public ClientCategory()
        {
            Client = new HashSet<Client>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Client> Client { get; set; }
    }
}
