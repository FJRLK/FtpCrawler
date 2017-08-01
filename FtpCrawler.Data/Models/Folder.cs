using System;
using System.Collections.Generic;

namespace FtpCrawler.Data.Models
{
    public class Folder : BaseEntity
    {
        public Int64 Id { get; set; }
        public Int64 ServerId { get; set; }
        public Int64? FolderId { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public String ShortName { get; set; }
        public String FullName { get; set; }


        public Folder ParentFolder { get; set; }
        public ICollection<Folder> SubFolder { get; set; }
        public ICollection<FtpFile> Files { get; set; }
        public FtpServer Server { get; set; }

    }
}