using Campaign_Module.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campaign_Module.Domain.AggregateModels
{
    public class Order : BaseEntity, IAggregateRoot
    {
        public int Quantity { get; set; }
        public string CampaignName { get; set; }
        public int Price { get; set; }

    }
}
