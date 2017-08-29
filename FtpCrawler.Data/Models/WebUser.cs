using System;

namespace FtpCrawler.Data.Models
{
    public class WebUser : BaseEntity
    {
        public Int64 Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public String UserName { get; set; }
        public String Password { get; set; }
        public Int32 UserType { get; set; }
    }
}