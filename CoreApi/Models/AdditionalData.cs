using System;
using System.Collections.Generic;

namespace CoreApi.Models
{
    public partial class AdditionalData
    {
        public int IndexPk { get; set; }
        public int IndexLevel { get; set; }
        public int MasterIndex { get; set; }
        public string AdditionalName { get; set; }
        public string CreaterUid { get; set; }
        public string IsDefault { get; set; }
        public string DefaultColor { get; set; }
        public string DefaultAmountColor { get; set; }
    }
}
