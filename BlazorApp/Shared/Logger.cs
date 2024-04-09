using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp.Shared
{
    public class Logger
    {
        String fileName = "log.txt";
        public void Log(String message)
        {
            System.IO.File.AppendAllText(fileName, message + "\n");
        }
    }
}
