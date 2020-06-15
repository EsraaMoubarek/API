using System;
using System.Collections.Generic;

namespace testwebapicore.Models
{
    public partial class Promotions
    {
        public Promotions()
        {
            PromotionCodes = new HashSet<PromotionCodes>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? RequiredPoints { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public string Details { get; set; }
        public int? CompanyId { get; set; }
        public string Image { get; set; }

        public virtual ComapnyPromotion Company { get; set; }
        public virtual ICollection<PromotionCodes> PromotionCodes { get; set; }
    }
}
