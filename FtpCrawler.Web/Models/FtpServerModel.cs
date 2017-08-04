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
        [Required(ErrorMessage ="Please enter a host name/ip!")]
        public String HostName { get; set; }
        [Required(ErrorMessage = "Please enter a user name!")]
        public String Login { get; set; }
        public String PassWord { get; set; }
        [Required(ErrorMessage = "Please enter a port number!")]
        public Int32 Port { get; set; }
        [Required(ErrorMessage = "Please enter a Starting directory! (default is '/')")]
        public String StartingDir { get; set; }
        public String FileList { get; set; }
        public String Comment { get; set; }
        public String EditableBy { get; set; }
    }
}