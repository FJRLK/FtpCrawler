using System;
using System.ComponentModel.DataAnnotations;

namespace FtpCrawler.Web.Models
{
    public class FtpServerModel
    {
        public FtpServerModel()
        {
            Port = 21;
            StartingDir = "/";
            FileList = "";
            EditableBy = "";
        }

        public Int64 EntryId { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public DateTime? LastOnline { get; set; }

        [Required(ErrorMessage = "Please enter a host name/ip!")]
        public String HostName { get; set; }

        [Required(ErrorMessage = "Please enter a user name!")]
        public String Login { get; set; }

        public String PassWord { get; set; }

        [Required(ErrorMessage = "Please enter a port number!")]
        [Range(0, Int32.MaxValue, ErrorMessage = "Please enter valid port Number")]
        public Int32 Port { get; set; }

        [Required(ErrorMessage = "Please enter a Starting directory! (default is '/')")]
        public String StartingDir { get; set; }

        public String FileList { get; set; }
        public String Comment { get; set; }
        public String EditableBy { get; set; }
        public Int64 TotalFileSize { get; set; }
        public Boolean Online { get { return LastOnline.HasValue && LastOnline.Value > DateTime.Now.AddMinutes(-30) ? true : false; } }
    }
}