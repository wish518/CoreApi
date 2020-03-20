using System;
using System.Collections.Generic;

namespace CoreApi.Models
{
    public partial class UserAdditional
    {
        public int IndexPk { get; set; }
        public int MasterIndex { get; set; }
        public string AdditionalName { get; set; }
        public string CreaterUid { get; set; }
    }
}
