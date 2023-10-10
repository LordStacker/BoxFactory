using Microsoft.Playwright.NUnit;
namespace Test;

public class Tests : PageTest
{

    [Test]
    public async Task TestGetBoxes()
    {
        using (HttpClient client = new HttpClient())
        {
            HttpResponseMessage response = await client.GetAsync("http://localhost:5000/boxes");

            
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

}