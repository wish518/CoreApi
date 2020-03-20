using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.SQL
{
    public partial class DefauleAmount
    {
        public decimal Amount { get; set; }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Srno { get; set; }
    }
}
