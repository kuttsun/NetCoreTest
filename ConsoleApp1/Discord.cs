using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ConsoleApp1
{
    public class Discord
    {
        static HttpClient client = new HttpClient();
        string _hookUrl;

        public Discord(string hookUrl)
        {
            _hookUrl = hookUrl;
        }

        public HttpResponseMessage Send(string message)
        {

            client.Timeout = new TimeSpan(0, 1, 0, 0);


            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var body = new StringContent($"{{ \"content\":\"{message}\" }}");
            body.Headers.ContentType.MediaType = "application/json";

            var response = client.PostAsync(_hookUrl, body).Result;
            //return response.IsSuccessStatusCode ? 1 : 0;
            return response;
        }
    }
}
