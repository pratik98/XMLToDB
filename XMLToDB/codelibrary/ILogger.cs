using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMLToDB.codelibrary
{
    public interface ILogger
    {
         void log(string entry);
    }


    public class FileLogger : ILogger
    {
      public void log(string trace)
        {
            System.IO.File.AppendAllText(@"d:\Error.txt", trace);
        }
    }
}