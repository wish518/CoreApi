using System;
using System.Collections.Generic;

namespace CoreApi.Models
{
    public partial class UserData
    {
        public string Id { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Sex { get; set; }
        public string Email { get; set; }
        public string Uid { get; set; }
        public string VaildId { get; set; }
        public DateTime CreateDateTime { get; set; }
        public DateTime LateLoginTime { get; set; }
        public string Authority { get; set; }
    }
}
