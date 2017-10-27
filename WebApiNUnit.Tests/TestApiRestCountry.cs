using Domain.Entities;
using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiNUnit.Tests
{
    [TestFixture]
    public class TestApiRestCountry
    {
        [Test]
        public void Test_Http_CountryController_Get()
        {
            var client = new RestClient("http://localhost:65413/api");
            // client.Authenticator = new HttpBasicAuthenticator(username, password);

            var request = new RestRequest("country", Method.GET);
            //request.AddParameter("name", "value"); // adds to POST or URL querystring based on Method
            //request.AddUrlSegment("id", "123"); // replaces matching token in request.Resource

            // easily add HTTP Headers
            //request.AddHeader("header", "value");

            // add files to upload (works with compatible verbs)
            //request.AddFile(path);

            // execute the request
            IRestResponse response1 = client.Execute(request);
            Console.WriteLine("Estado");
            Console.WriteLine(response1.StatusCode);
            var content = response1.Content; // raw content as string
            Console.WriteLine("Response 1");
            Console.WriteLine(content);

            // or automatically deserialize result return content type is sniffed but can be explicitly set via RestClient.AddHandler();
            var response2 = client.Execute<List<Country>>(request);
            var lista = response2.Data;
            Console.WriteLine(lista.Count);
            Console.WriteLine("Response 2");
            Console.WriteLine( content);

            //// easy async support
            //Console.WriteLine("Response 3");
            //client.ExecuteAsync(request, response3 => {
            //    Console.WriteLine(response3.Content);
            //});

            //// async with deserialization
            //Console.WriteLine("Response 4");
            //var asyncHandle = client.ExecuteAsync<List<Country>>(request, response4 => {
            //    Console.WriteLine(response4.Data.Count);
            //});

            // abort the request on demand
            //asyncHandle.Abort();


        }
    }
}
