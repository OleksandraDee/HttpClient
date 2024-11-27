using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HttpClientDemo
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using HttpClient client = new HttpClient();

            // Base URL for the mock API
            string baseUrl = "https://jsonplaceholder.typicode.com/posts";

            // 1. GET Request
            await GetRequest(client, $"{baseUrl}/1");

            Console.WriteLine("\n---\n");

            // 2. POST Request
            await PostRequest(client, baseUrl);

            Console.WriteLine("\n---\n");

            // 3. PUT Request
            await PutRequest(client, $"{baseUrl}/1");

            Console.WriteLine("\n---\n");

            // 4. DELETE Request
            await DeleteRequest(client, $"{baseUrl}/1");

            Console.WriteLine("\n---\n");

            // 5. PATCH Request
            await PatchRequest(client, $"{baseUrl}/1");
        }

        private static async Task GetRequest(HttpClient client, string url)
        {
            Console.WriteLine("GET Request:");
            var response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                Console.WriteLine(data);
            }
            else
            {
                Console.WriteLine($"GET failed with status code: {response.StatusCode}");
            }
        }

        private static async Task PostRequest(HttpClient client, string url)
        {
            Console.WriteLine("POST Request:");
            var payload = new StringContent(
                "{\"title\": \"foo\", \"body\": \"bar\", \"userId\": 1}",
                Encoding.UTF8,
                "application/json");

            var response = await client.PostAsync(url, payload);
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                Console.WriteLine(data);
            }
            else
            {
                Console.WriteLine($"POST failed with status code: {response.StatusCode}");
            }
        }

        private static async Task PutRequest(HttpClient client, string url)
        {
            Console.WriteLine("PUT Request:");
            var payload = new StringContent(
                "{\"id\": 1, \"title\": \"foo\", \"body\": \"bar\", \"userId\": 1}",
                Encoding.UTF8,
                "application/json");

            var response = await client.PutAsync(url, payload);
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                Console.WriteLine(data);
            }
            else
            {
                Console.WriteLine($"PUT failed with status code: {response.StatusCode}");
            }
        }

        private static async Task DeleteRequest(HttpClient client, string url)
        {
            Console.WriteLine("DELETE Request:");
            var response = await client.DeleteAsync(url);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Resource deleted successfully.");
            }
            else
            {
                Console.WriteLine($"DELETE failed with status code: {response.StatusCode}");
            }
        }

        private static async Task PatchRequest(HttpClient client, string url)
        {
            Console.WriteLine("PATCH Request:");
            var payload = new StringContent(
                "{\"title\": \"Updated Title\"}",
                Encoding.UTF8,
                "application/json");

            var request = new HttpRequestMessage(new HttpMethod("PATCH"), url) { Content = payload };
            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                Console.WriteLine(data);
            }
            else
            {
                Console.WriteLine($"PATCH failed with status code: {response.StatusCode}");
            }
        }
    }
}
