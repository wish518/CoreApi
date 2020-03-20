using System;
using System.Collections.Generic;

namespace CoreApi.Models
{
    public partial class HtmlCssSetDetail
    {
        public string Uid { get; set; }
        public string PageCode { get; set; }
        public string CssTag { get; set; }
        public string Css { get; set; }
        public string Name { get; set; }
        public string CssName { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string IsSet { get; set; }
    }
}
