using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FtpCrawler.Runner
{
    class Program
    {
        static void Main(string[] args)
        {
            

            foreach (Type thread in GetTypesInNamespace(Assembly.GetExecutingAssembly(), "FtpCrawler.Runner.Threads"))
            {
                if (thread.Inherits(typeof(ITaskThread)))
                {
                    ThreadActionKeyAttribute attr = thread.GetCustomAttributes<ThreadActionKeyAttribute>().FirstOrDefault();

                    if (null != attr && !String.IsNullOrEmpty(attr.ActionKey) )
                    {
                        RunThread(thread);
                    }
                }
            }
        }

        private static void RunThread(Type thread)
        {
            Logger log = new Logger(thread.Name);
            log.Log(String.Format("Creating instance of '{0}'", thread.Name));

            try
            {
                using (ITaskThread instance = (ITaskThread)Activator.CreateInstance(thread))
                {
                    instance.SetLogger(log);

                    log.Log(String.Format("Running: {0}", instance.ActionName));
                    instance.Run();

                    log.Log(String.Format("Done: {0}", instance.ActionName));
                }
            }
            catch (Exception ex)
            {
                log.Log(String.Format("Error running '{0}'", thread.Name));
                log.LogException(ex);
            }
        }

        static private IEnumerable<Type> GetTypesInNamespace(Assembly assembly, String nameSpace)
        {
            return assembly.GetTypes().Where(t => String.Equals(t.Namespace, nameSpace, StringComparison.Ordinal));
        }
    }
    internal static class Extensions
    {
        public static Boolean Inherits(this Type t1, Type t2)
        {
            return t2.IsAssignableFrom(t1);
        }
    }
}
