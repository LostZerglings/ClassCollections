using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Alice_Stand_Alone.Class_Storage
{
    /// <summary>
    /// 
    /// Description:
    /// This class would allow you to create an incident in most iinstances of service now. It would require some modifications to work. 
    /// 
    /// First: 
    /// You need to specify your "Service-Now Username" and "Service-Now Password" in the code below. This is assuming you have service now setup to use basic authentication.
    /// This account should have access to the service-now REST API. Other setups of service-now may require differnet or no authentication. 
    /// 
    /// Second: 
    /// You need to specify your "Service-Now Instance" name. This is in your url when you access service-now. https://{your instance}.service-now.com.
    /// 
    /// Lastly: 
    /// Under "postBody" you need to specify the contents you want to post. This should be formatted to post in Json. The example below simply adds 
    /// a short description and a caller to show an example of how it would work. Ultimately you would want to pass some content to the SendToServiceNow method 
    /// that contains your dynamic criteria to allow you to reuse this method.
    /// 
    /// Usage: 
    /// string Incident = await PostToServiceNow.SendToServiceNow()
    /// 
    /// The expected result is that Incident would equal the JSON text of the created incident in your service now instance. 
    /// You can then parse through this as you need.
    /// 
    /// </summary>

    class PostToServiceNow
    {

        public static async Task<string> SendToServiceNow()
        {
            // First supply your username password and your instance that would be used by service now.
            //
            // This can easily be refactored to use more secure code, this is just to serve as an example. 

            string serviceNowUsername = "your service-now username";
            string serviceNowPassword = "your service-now password";
            string serviceNowInstance = "your service-now instance";

            // Next we want to specify the postBody. This is the content we want to show up in our newly created ticket. 
            //
            // This should be sent in JSON formatting. While different instances of service now may require you to modify this 
            // the idea is {"field_name":"field_value"}. You can use commas to delineate multiple values.
            //
            // Keep in mind we need to escape the " value unless you are using a JSON converter.

            string postBody = "{\"short_description\": \"" + "Your short description goes here" + "\" , \"caller_id\": \"" + "Your caller ID goes here" + "\" , \"description\": \"}";

            // Now we want to send the content. We will do this by sending an HTTP message. In order to do that we need a client. 
            //
            // The flow is that we create the client. We then send the headers (authentication) then we send our message.
            //
            // We can then "await" the response from service-now. This response would contain the entire JSON of the incident. 
            //
            // You can than parse through the content as needed. 
             

            using (HttpClient client = new HttpClient())
            {
                // Now that we have a client we need to convert our username and password to a byteArray to allow us to send it in the header
                //
                // Once we have created the array add it to the authentication portion of the headers as shown below. Ensure to speciy we are using "Basic" authentication. 
                //
                // Note: If your instance of Service-Now does not use basic authentication or requires no authentication you would have to modify these lines as needed.


                var byteArray = Encoding.ASCII.GetBytes($"{serviceNowUsername}:{serviceNowPassword}");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

                // Next we need to specify we are the data is going to be "application/json"
                //
                // This allows us to send and receive JSON formated data.

                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                // Now that the headers are out of the way we can get to the message.
                //
                // We are using a "post" message here. You can easily modify this to use "get" or "put" messages however you will need to adapt the url to include the sys_id of the target incident. 
                // 
                // In this example we are just going to post content so a sys_id is not required. We just need to specify the tablet we want to post data to. In most cases this will be the incident table.
                //
                // Format the message as shown below. Be sure to include your incident table path. If you customized service-now it may be different. Also be sure to specify the data is application/json.

                using (HttpResponseMessage response = await client.PostAsync($"https://{serviceNowInstance}.service-now.com/api/now/v1/table/incident", new StringContent(postBody, Encoding.UTF8, "application/json")))
                {
                    using (HttpContent content = response.Content)
                    {
                        // Now that the post request has been made we wait to await the response content.
                        //
                        // This will be formated in JSON and will include all the details of your incident that has been created. 
                        //
                        // If the process failed it'll include an error message or status. 

                        return await content.ReadAsStringAsync();
                    }
                }
            }
        }
    }
}
