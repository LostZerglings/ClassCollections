using System;
using System.Collections.Generic;
using System.Management.Automation;


namespace Program
{
    /// <summary>
    /// 
    /// Description:
    /// This class shows usage of powershell within C# code. It can easily be addapted to run different commands or scripts of varrying complexity. 
    /// 
    /// Note: 
    /// you need to add the System.Management.Automation.dll refference to use the powershell commands.
    /// To install this dll in visual studio go to Project > Manage nu get packages then type System.Management.Automation.dll in the search. 
    /// Once you have installed it don't forget to add using System.Management.Automation; to your project. 
    /// 
    /// Additionally the device you run this on needs to be connected to a Active directory database or have RSAT installed to allow you to connect to a remote one.
    ///
    /// It also may be advantageous to convery the method to an async method since retriving information can take time depending 
    /// on how much data you are retriving and where you are getting it from.
    /// 
    /// Usage:
    /// This class requires one argument that is the username of the user you want to run the command against. In powershell this would look like 
    /// "get-aduser $username" to make this script more useful we can add "-properties *" to include all properties of this individual. 
    /// 
    /// var userinfo = PowershellGetADUser.getADUser("username")
    /// 
    /// In the foreach portion of the ps object you would determine what you want to do with this data, you. For this example we will just add the users name,
    /// and email address to a list. This can easily be modified later to match your needs.
    /// 
    /// 
    /// </summary>

    public static class PowershellGetADUser
    {
        public static List<string> getADUser(string username)
        {
            // For this example we will be getting and returning the user information in a list. 
            //
            // To create a new list we define it as such:

            List<string> userinfo = new List<string>();

            // Now that we have somewhere to put the data lets get our powershell instance up and running. 
            //
            // We want to use a using block here to ensure we close out of the powershell session when we are finished. 
            //
            // Once we have added the system management automation dll setting up a powershell session is as simple as Powershell.Create()

            using (PowerShell ps = PowerShell.Create())
            {
                // Now that we have a powershell session we need to tell it what we want to run. This can be done a couple ways. 
                //
                // For simple commands you can do ps.AddCommand("command-name")
                //
                // Or you can list an entire script by doing ps.AddScript("script")
                //
                // And lastly you can chain commands, arguments, and parameters together. 
                //
                // For example ps.AddCommand("command-name").AddArgument("argument").AddParameter("Param Name" , "Param Value");
                //
                // For this example we will chain the script together like so.

                ps.AddCommand("Get-ADuser").AddArgument(username).AddParameter("Properties", "*");

                // Now that we have a script we need to run it in powershell. To do this we need to invoke it. 
                //
                // Since we want to retrive and look at the objects we can do that in one like like this.

                foreach (PSObject result in ps.Invoke())
                {
                    // This will go through each object in the results. 
                    //
                    // For this example we are only looking for the name, and email. 
                    //
                    // Since these are psobjects i'm going to convert them to a string as well to allow them to be added to my list. 
                    //
                    // Keep in mind you may need to check and confirm the field names to make sure that you are entering them correctly.
                    //
                    // We are going to add a label prefix to the values to make them more readable and then add them to our list. 

                    userinfo.Add($"Name: {Convert.ToString(result.Members["name"].Value)}");
                    userinfo.Add($"Email: {Convert.ToString(result.Members["mail"].Value)}");
                }
            }
            // And finally we return our userdata. 
            return userinfo;
        }
    }
}
