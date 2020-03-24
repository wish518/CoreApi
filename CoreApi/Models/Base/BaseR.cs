using System;

namespace CoreApi.Models
{
    public class BaseR<T>
    {
        private string Default_Error = "Y";
        private DateTime Default_SeverDatetime = Convert.ToDateTime("1900/01/01");
        /// <summary> 
        /// </summar
        public string IS_Error
        {
            get { return Default_Error; }
            set { Default_Error = value; }
        }

        /// <summary> 
        /// </summar
        public string MSG { get; set; }

        /// <summary> 
        /// </summar
        public string Status { get; set; }

        /// <summary> 
        /// </summar
        public string UID { get; set; }

        public T Data { get; set; }

        public string Token { get; set; }

        /// <summary> 
        /// </summar
        public DateTime SeverDatetime { get { return Default_SeverDatetime; } set { Default_SeverDatetime = value; } }
    }
}
