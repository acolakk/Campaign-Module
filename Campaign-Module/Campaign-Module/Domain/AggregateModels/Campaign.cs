using Campaign_Module.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campaign_Module.Domain.AggregateModels
{
    public class Campaign : BaseEntity, IAggregateRoot
    {
        public string CampaignName { get; set; }
        public int Duration { get; set; }
        public int PriceManipulationLimit { get; set; }
        public int TargetSalesCount { get; set; }
    }
}
