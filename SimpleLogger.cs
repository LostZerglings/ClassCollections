using System;
using System.IO;

namespace Alice_Stand_Alone.Class_Storage
{
    public class Log
    {
        public static void writelog(string text)
        {
            string filepath = "your path goes here";

            try
            {
                using (StreamWriter writer = new StreamWriter(filepath, true))
                {
                    writer.WriteLine($"{Environment.UserName} {text} {DateTime.Now}");
                }
            }
            catch (Exception error)
            {
                //how you want to handle any errors goes here. 
            }
        }
    }

}
