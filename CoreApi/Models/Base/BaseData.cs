using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApi.Models
{
    public class BaseData<T>
    {
        public string Uid { get; set; }

        public T Data { get; set; }
    }
}
