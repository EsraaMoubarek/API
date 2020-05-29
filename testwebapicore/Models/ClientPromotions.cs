using System;
using System.Collections.Generic;

namespace testwebapicore.Models
{
    public partial class ClientPromotions
    {
        public int ClientId { get; set; }
        public int? PromotionId { get; set; }
        public int Id { get; set; }
        public DateTime? Date { get; set; }

        public virtual Promotions Promotion { get; set; }
    }
}
