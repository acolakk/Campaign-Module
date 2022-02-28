using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Campaign_Module.Domain.SeedWork
{
    public interface IRepository<T> where T : BaseEntity
    {
        T GetByProductCode(string ProductCode);
        ResultMessage Add(T entity);
        List<T> Get(Expression<Func<T, bool>> filter = null);
    }
}
