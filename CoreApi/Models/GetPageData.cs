using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApi.Models
{
    public class GetPageData
    {
        public List<PageData> PageDataList { get; set; }
        public List<DropDownData> DropDownData { get; set; }
    }
}
