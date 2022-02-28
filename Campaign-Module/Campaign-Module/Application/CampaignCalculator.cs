using Campaign_Module.Domain.AggregateModels;
using Campaign_Module.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campaign_Module.Application
{
    public class CampaignCalculator
    {
        public int Calculate(Product product)
        {
            RepositoryProvider provider = new RepositoryProvider();
            var campaignData = provider.CampaignRepository.GetByProductCode(product.ProductCode);
            int time = Program.time;
            if (campaignData != null && campaignData.Duration >= time && time != 0)
            {
                int discount = (campaignData.PriceManipulationLimit / ((campaignData.Duration - 1)) * time);
                return (product.Price - discount);
            }
            else
            {
                return product.Price;
            }
        }

        
    }
}
