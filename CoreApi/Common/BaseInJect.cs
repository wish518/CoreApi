using CoreApi.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApi.Common
{
    public class BaseInJect
    {
        public WISHContext _Context;
        public IConfiguration _configuration;
        public BaseInJect(IConfiguration configuration, WISHContext Context)
        {
            _configuration = configuration;
            _Context = Context;
        }
    }
}
