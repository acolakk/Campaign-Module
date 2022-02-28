using Campaign_Module.Domain.AggregateModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campaign_Module.Domain.SeedWork
{
    public interface IUnitOfWork
    {
        IRepository<Campaign> CampaignData { get; }
        IRepository<Order> OrderData { get; }
        IRepository<Product> ProductData { get; }

    }
}
