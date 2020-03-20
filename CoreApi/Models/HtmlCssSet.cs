using System;
using System.Collections.Generic;

namespace CoreApi.Models
{
    public partial class HtmlCssSet
    {
        public string PageCode { get; set; }
        public string CssTag { get; set; }
        public string CssTagName { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Srno { get; set; }
    }
}
