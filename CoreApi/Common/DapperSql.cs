using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApi.Common
{
    public class DapperSql
    {
        public SqlConnection conn;

        public DapperSql(IConfiguration configuration) {
            string strConnection = configuration.GetConnectionString("WISH");
            conn = new SqlConnection(strConnection);
        }
    }
}
