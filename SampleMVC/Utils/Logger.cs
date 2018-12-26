using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace SampleMVC.Utils
{
    public class Logger
    {
        public void ExceptionLogger(Exception ex)
        {
            try
            {
                var path = Path.Combine(HttpRuntime.AppDomainAppPath, "App_Data/Logs/Exceptionlog.txt");
                var file = new FileInfo(path);
                file.Directory.Create();               

                using (StreamWriter writer = new StreamWriter(path, true))
                {
                    writer.WriteLine("-----------------------------------------------------------------------------");
                    writer.WriteLine("Date : " + DateTime.Now.ToString());
                    writer.WriteLine();

                    while (ex != null)
                    {
                        writer.WriteLine(ex.GetType().FullName);
                        writer.WriteLine("Message : " + ex.Message);
                        writer.WriteLine("StackTrace : " + ex.StackTrace);

                        ex = ex.InnerException;
                    }
                }               

            }
            catch (Exception )
            {
                //Console.WriteLine(e.Message);
            }

        }


        public void TestLogger(string data)
        {
            try
            {
                var path = Path.Combine(HttpRuntime.AppDomainAppPath, "App_Data/Logs/test.txt");
                var file = new FileInfo(path);
                file.Directory.Create();

                using (StreamWriter writer = new StreamWriter(path, true))
                {
                    writer.WriteLine("-----------------------------------------------------------------------------");
                    writer.WriteLine("Date : " + DateTime.Now.ToString());
                    writer.WriteLine(data);

                    
                }
            }
            catch (Exception)
            {
                //Console.WriteLine(e.Message);
            }

        }
    }
}