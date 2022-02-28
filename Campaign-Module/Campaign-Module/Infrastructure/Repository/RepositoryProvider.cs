using Campaign_Module.Domain.AggregateModels;
using Campaign_Module.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campaign_Module.Infrastructure.Repository
{
    public class RepositoryProvider 
    {
        private IRepository<Campaign> campaignRepository { get; set; }
        public IRepository<Campaign> CampaignRepository
        {
            get
            {
                return campaignRepository ??
                    (campaignRepository = new BaseRepository<Campaign>());
            }
        }

        private IRepository<Order> orderRepository { get; set; }
        public IRepository<Order> OrderRepository
        {
            get
            {
                return orderRepository ??
                    (orderRepository = new BaseRepository<Order>());
            }
        }

        private IRepository<Product> productRepository { get; set; }
        public IRepository<Product> ProductRepository
        {
            get
            {
                return productRepository ??
                    (productRepository = new BaseRepository<Product>());
            }
        }
    }
}
