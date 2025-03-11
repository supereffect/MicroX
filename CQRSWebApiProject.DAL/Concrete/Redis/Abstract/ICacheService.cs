using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRSWebApiProject.DAL.Concrete.Redis.Abstract
{
    public interface ICacheService
    {
        public void SetCache(string key, string value, TimeSpan? expiry);

        public string GetCache(string key);
    }
}
