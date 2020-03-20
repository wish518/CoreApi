using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.SQL
{
    public partial class UserAdditional
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IndexPk { get; set; }
        public int MasterIndex { get; set; }
        public string AdditionalName { get; set; }
        public string CreaterUid { get; set; }
    }
}
