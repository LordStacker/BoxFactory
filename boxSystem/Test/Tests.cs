using System.Text;
using Microsoft.Playwright.NUnit;
using Newtonsoft.Json;

namespace Test;

public class Tests : PageTest
{

    [Test]
    public async Task TestGetBoxes()
    {
        using (HttpClient client = new HttpClient())
        {
            HttpResponseMessage response = await client.GetAsync($"{Helper.ClientAppBaseUrl}/boxes");

            
            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();
                Assert.IsFalse(string.IsNullOrWhiteSpace(responseContent), "API response should not be empty.");
            }
            else
            {
                Assert.Fail($"API request failed with status code: {response.StatusCode}");
            }
        }
    }
    
    [Test]
    public async Task TestGetBoxById()
    {
        using (HttpClient client = new HttpClient())
        {
            int boxId = 84;
            HttpResponseMessage response = await client.GetAsync($"{Helper.ApiBaseUrl}/{boxId}");

            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();
                Assert.IsFalse(string.IsNullOrWhiteSpace(responseContent), "API response should not be empty.");
            }
            else
            {
                Assert.Fail($"API request failed with status code: {response.StatusCode}");
            }
            
        }
    }
    [Test]
    public async Task TestPostBox()
    {
        using (HttpClient client = new HttpClient())
        {
            var boxData = new Boxes()
            {
                BoxName = "boks fra prøve",
                Material = "prøve",
                Width = 10.0m,
                Height = 5.0m,
                Depth = 3.0m
            };
            string jsonPayload = JsonConvert.SerializeObject(boxData);
            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync(Helper.ApiBaseUrl, content);

            Assert.IsTrue(response.IsSuccessStatusCode, "POST request should be successful.");
        }
    }
    /*
    [Test]
    public async Task TestDeleteBox()
    {
        using (HttpClient client = new HttpClient())
        {
            int boxId = 85; 

            HttpResponseMessage response = await client.DeleteAsync($"{Helper.ApiBaseUrl}/{boxId}");

            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();
                Assert.IsFalse(string.IsNullOrWhiteSpace(responseContent), "API response should not be empty.");
            }
            else
            {
                Assert.Fail($"API request failed with status code: {response.StatusCode}");
            }
        }
    }*/


}