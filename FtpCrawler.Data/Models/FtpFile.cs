using System;
using System.Collections.Generic;

namespace FtpCrawler.Data.Models
{
    public class FtpFile : BaseEntity
    {
        public Int64 Id { get; set; }
        public Int64 ServerId { get; set; }
        public Int64 FolderId { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public String ShortName { get; set; }
        public String FullName { get; set; }
        public String Extension { get; set; }
        public Int64 FileSize { get; set; }
        public DateTime FileDate { get; set; }

        public Folder Folder { get; set; }
        public FtpServer Server { get; set; }
    }
}