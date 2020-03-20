using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.SQL
{
    public partial class AdditionalData
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IndexPk { get; set; }
        public int IndexLevel { get; set; }
        public int MasterIndex { get; set; }
        public string AdditionalName { get; set; }
        public string CreaterUid { get; set; }
        public string IsDefault { get; set; }
        public string DefaultColor { get; set; }
    }
}
