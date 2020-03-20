﻿using System;
using System.Collections.Generic;

namespace CoreApi.Models
{
    public partial class AccoutingRecord
    {
        public string AllotNo { get; set; }
        public string Uid { get; set; }
        public int IndexPk { get; set; }
        public DateTime AccoutingDate { get; set; }
        public int ChildIndexPk { get; set; }
        public string Additional { get; set; }
        public decimal Amount { get; set; }
        public int Day { get; set; }
        public DateTime Time { get; set; }
    }
}
