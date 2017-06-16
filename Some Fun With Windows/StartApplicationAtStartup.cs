using System;
using Microsoft.Win32;
using System.IO;

namespace Program
{
    /// <summary>
    /// 
    /// Description:
    /// This class will modify the load behavior of a designated program to start when windows loads.
    /// 
    /// Usage:
    /// StartApplicationAtStartup.setLoadBehavior();
    /// 
    /// When you restart windows the application should load automatically. 
    /// 
    /// </summary>

   public class StartApplicationAtStartup
    {
        public void setLoadBehavior()
        {
            // Use a try and catch block to catch any errors that occur when modifying the registry. 
            //
            // You can handle them anyway you like. The most common error would likely be that a user is not an admin of the device. 
            //
            // Only an admin would be able to modify a registry value required to set the loadup behavior.
            //
            // There are a couple options for structuring this method. You could pass an argument and get the application name that way. 
            // 
            // You could hard code the application name as a string. 
            //
            // Or use system reflection to get the currently running application. For our example we will do this. 
            //
            // We do this by using getfile name with the reflection. This will return the name of the exe so we will need to remove the .exe from the end.

            string applicationName = Path.GetFileName(System.Reflection.Assembly.GetEntryAssembly().Location).Replace(".exe","");

            try
            {
                // The registry key "run" located at the path below is a current user key that sets startup programs. This will ensure we only modify
                // the behavior for the current user. Not everyone who signs into the device.
                //
                // In order to open a registry key ensure you have added using Microsoft.Win32; to your project. 
                //
                // In addition to specifying the path ensure to add a "true" value to allow the key to be edited.
                //
                // Lastly ensure to encompass the registry key in a using block. We want to ensure we dispose of the instance when we are done to ensure the key closes. 

                using (RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
                {

                    // Now that we have the registry key that contains a list of the programs to load at startup we want to see if a key exists for the application name
                    //
                    // We use a "Getvalue" command to check if there is a key containing our applicaiton name 
                    //
                    // If that result is null that means the value doesn't exist and we need to create the key value.
                    //
                    // You can use the application name and system reflection to set the key name and key value. 

                    if (key.GetValue(applicationName) == null)
                    {
                        key.SetValue(applicationName, System.Reflection.Assembly.GetExecutingAssembly().Location);
                    }
                }
            }
            catch (Exception error)
            {
                //Handle any errors here
            }
        }

    }
}
