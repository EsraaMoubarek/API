using System;
using System.Collections.Generic;

namespace testwebapicore.Models
{
    public partial class PromotionCodes
    {
        public int? ClientId { get; set; }
        public int PromtionId { get; set; }
        public string Code { get; set; }

        public virtual Client Client { get; set; }
        public virtual Promotions Promtion { get; set; }
    }
}
