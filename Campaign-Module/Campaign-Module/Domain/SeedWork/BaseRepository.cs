using Campaign_Module.Domain.AggregateModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Campaign_Module.Domain.SeedWork
{
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        private string tableName { get; set; }
        public BaseRepository()
        {
            tableName = typeof(T).Name;
        }

        public ResultMessage Add(T entity)
        {
            ResultMessage result = new ResultMessage();
            string dataPath = Path.Combine(Environment.CurrentDirectory, @"JsonData\", tableName + ".json");

            if (GetByProductCode(entity.ProductCode) == null)
            {


                string data = File.ReadAllText(dataPath);

                if (string.IsNullOrEmpty(data))
                {
                    data = JsonConvert.SerializeObject(entity);
                    data = "[" + data + "]";

                    File.WriteAllText(dataPath, data);
                }
                else
                {
                    var jsonData = JsonConvert.DeserializeObject<List<dynamic>>(data);
                    jsonData.Add(entity);
                    data = JsonConvert.SerializeObject(jsonData);
                    File.WriteAllText(dataPath, data);
                }

                result.Result = true;

            }
            else
            {
                result.Result = false;
                result.Message = "This product code already exists";
            }
            return result;
        }

        public T GetByProductCode(string ProductCode)
        {
            string dataPath = Path.Combine(Environment.CurrentDirectory, @"JsonData\", tableName + ".json");
            string data = File.ReadAllText(dataPath);

            if (!string.IsNullOrEmpty(data))
            {
                var result = JsonConvert.DeserializeObject<List<T>>(data).Find(x => x.ProductCode == ProductCode);


                return result;
            }
            else
            {
                return default(T);
            }
        }

        public virtual List<T> Get(Expression<Func<T, bool>> filter = null)
        {

            string dataPath = Path.Combine(Environment.CurrentDirectory, @"JsonData\", tableName + ".json");
            string data = File.ReadAllText(dataPath);

            if (!string.IsNullOrEmpty(data))
            {
                var result = JsonConvert.DeserializeObject<List<T>>(data);

                if (filter != null)
                {
                    result = result.AsQueryable().Where(filter).ToList();
                }
                return result;
            }
            else
            {
                return new List<T>();
            }

        }



    }
}
