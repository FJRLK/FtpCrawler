using System;
using System.Collections.Generic;

namespace FtpCrawler.Data.Models
{
    [Serializable]
    public class FtpServer : BaseEntity
    {
        public Int64 Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public String HostName { get; set; }
        public String Login { get; set; }
        public String PassWord { get; set; }
        public Int32 Port { get; set; }
        public String StartingDir { get; set; }
        public String FileList { get; set; }
        public String Comment { get; set; }
        public String EditableBy { get; set; }

        public ICollection<Folder> Folders { get; set; }
        public ICollection<FtpFile> Files { get; set; }
    }
}