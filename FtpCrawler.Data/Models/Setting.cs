using System;

namespace FtpCrawler.Data.Models
{
    public class Setting : BaseEntity
    {
        public Int64 Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public String Key { get; set; }
        public String Type { get; set; }
        public String Value { get; set; }
    }
}