using System;
using System.Threading.Tasks;
using OpenTelemetry;
using System.Net.Http;
using OpenTelemetry.Trace;
using OpenTelemetry.Instrumentation.Http;

namespace client
{
    class Program
    {

        static readonly HttpClient client = new HttpClient();
        static async Task Main(string[] args)
        {
            // Configure OpenTelemetry Tracer with Console exported and initiate it
            Sdk.CreateTracerProviderBuilder()
            .AddHttpClientInstrumentation()
            .AddConsoleExporter()
            .Build();

            try
            {
                // Simulate HTTP Request to our service
                string responseBody = await client.GetStringAsync("https://localhost:5001/weatherforecast/abc");

                Console.WriteLine(responseBody);

            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }

            Console.WriteLine("Done!");
        }
    }
}
