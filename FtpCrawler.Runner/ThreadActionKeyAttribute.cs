using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FtpCrawler.Runner
{
    internal class ThreadActionKeyAttribute : Attribute
    {
        public String ActionKey;

        public ThreadActionKeyAttribute(String key)
        {
            this.ActionKey = key;
        }
    }
}
