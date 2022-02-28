using Campaign_Module.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campaign_Module.Domain.AggregateModels
{
    public class Product : BaseEntity, IAggregateRoot
    {

        public int Price { get; set; }
        public int Stock { get; set; }

    }
}
