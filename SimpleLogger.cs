using System;
using System.IO;

namespace Program
{
    /// <summary>
    /// 
    /// Description: 
    /// A simple logging class that receives the log as a string, appends the user who logged it and the date and time it occurred, then writes it to a file.
    ///
    /// Usage:
    /// First you will need to add a file path to the code below. Once it's been added you can call the class by adding the following to your code:
    /// Log.writelog("log text goes here");
    /// 
    /// The result will be that a log containing your username + the log text + the date and time will be written to the file you specified.
    ///
    /// </summary>
    
    public class Log
    {
        public static void writelog(string text)
        {
            // First we want to give the class a hard coded path to write logs to. 
            //
            // You could make unique user logs by adding Environment.UserName to the path.
            // Example: path = $@"\\path\to\your\log\folder\{Environment.UserName}log.txt
            //
            // Or just log to a single location by specifying a single path.
            //
            // Keep in mind this is using a stream writer and we aren't buffering anything so if the file is open
            // or locked by another process or we try to run it from two locations at the same time we won't be 
            // able to write the log.
            
            string filepath = "your path goes here";

            // We can use a try and catch block to catch any errors when writing the log. 
            
            try
            {
                // By wrapping the streamwriter in a using block we ensure that it is disposed once we finish writing.
                //
                // This helps manage resources and also ensures that we don't lock the file for future logs.
                //
                // When creating the streamwriter be sure to append true as the second argument. This allows us 
                // to append to the log. If you forget this your log will overwrite the existing log each time it runs
                // resulting in a single (most recent) log being stored instead of a history of logs. 
                
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
