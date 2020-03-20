using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.SQL
{
    public partial class UserData
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Sex { get; set; }
        public string Email { get; set; }
        public string Uid { get; set; }
        public string VaildId { get; set; }
        public DateTime CreateDateTime { get; set; }
        public DateTime LateLoginTime { get; set; }
    }
}
