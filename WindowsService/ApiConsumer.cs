using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace WindowsService
{
    public class ApiConsumer
    {
        public async Task<string> ConsumeAPI1()
        {
            var app = ConfidentialClientApplicationBuilder
               .Create("ca9bf9be-48ee-4320-9473-a6d75a8d83d0")
               .WithClientSecret("B78c?RLo/hv]af/tLQLFibaYEkomHA16")
               .WithAuthority(new Uri("https://login.microsoftonline.com/iqunlock.onmicrosoft.com"))
               .Build();

            // This scope contains a Guid which is an Application ID of WebAPI1 application.
            var scopes = new List<string>
            {
                "https://iqunlock.onmicrosoft.com/821eed60-5f85-49c8-8a9e-981e5e9097df/.default"
            };

            var graphResponse = app.AcquireTokenForClient(scopes).ExecuteAsync().Result;

            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", graphResponse.AccessToken);

            // Call the web API.
            var response = await httpClient.GetAsync("https://localhost:44340/api/weatherforecast");

            var result = await response.Content.ReadAsStringAsync();

            return result;
        }

        public async Task<string> ConsumeAPI2()
        {
            var app = ConfidentialClientApplicationBuilder
               .Create("ca9bf9be-48ee-4320-9473-a6d75a8d83d0")
               .WithClientSecret("B78c?RLo/hv]af/tLQLFibaYEkomHA16")
               .WithAuthority(new Uri("https://login.microsoftonline.com/iqunlock.onmicrosoft.com"))
               .Build();

            // This scope contains a Guid which is an Application ID of WebAPI2 application.
            var scopes = new List<string>
            {
                "https://iqunlock.onmicrosoft.com/a41db2a0-89ce-40c1-b17c-f86c473bbb43/.default"
            };

            var graphResponse = app.AcquireTokenForClient(scopes).ExecuteAsync().Result;

            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", graphResponse.AccessToken);

            // Call the web API.
            var response = await httpClient.GetAsync("https://localhost:44392/api/weatherforecast");

            var result = await response.Content.ReadAsStringAsync();

            return result;
        }
    }
}