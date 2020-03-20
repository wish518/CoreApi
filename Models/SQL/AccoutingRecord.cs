using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.SQL
{
    public partial class AccoutingRecord
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
