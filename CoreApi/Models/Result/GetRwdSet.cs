using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApi.Models
{
    public class GetRwdSet
    {
        public List<HtmlCssSet> HtmlCssSetData { get; set; }
        public List<HtmlCssSetDetail> HtmlCssSetDetailData { get; set; }
        public List<HtmlSet> HtmlSetData { get; set; }
        
    }
}
