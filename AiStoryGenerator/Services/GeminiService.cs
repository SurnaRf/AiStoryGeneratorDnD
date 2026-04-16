using System.Text;
using System.Text.Json;

namespace AiStoryGenerator.Services;

public class GeminiService
{
	private readonly HttpClient _httpClient = new HttpClient();

	private string apiKey = "AIzaSyD5-V3Ay3Znqqkn_JouU7F-SBjUNgL0cF4";
	private string apiKey2 = "AQ.Ab8RN6K1CuPnE8P2RUYssap8u50IDFXo7eUdFX0RAcF0hTvt_g";

	public async Task<string> GenerateStory(string prompt)
	{
		var requestBody = new
		{
			contents = new[]
			{
			new
			{
				parts = new[]
				{
					new { text = prompt }
				}
			}
		}
		};

		string json = JsonSerializer.Serialize(requestBody);

		var response = await _httpClient.PostAsync(
			$"https://generativelanguage.googleapis.com/v1/models/gemini-2.5-flash:generateContent?key={apiKey2}",
			new StringContent(json, Encoding.UTF8, "application/json")
		);

		var responseString = await response.Content.ReadAsStringAsync();

		using JsonDocument doc = JsonDocument.Parse(responseString);

		if (doc.RootElement.TryGetProperty("error", out JsonElement error))
		{
			return "Gemini error:\n\n" + error.ToString();
		}

		if (!doc.RootElement.TryGetProperty("candidates", out JsonElement candidates))
		{
			return "Unexpected response:\n\n" + responseString;
		}

		var text = candidates[0]
			.GetProperty("content")
			.GetProperty("parts")[0]
			.GetProperty("text")
			.GetString();

		return text;
	}
}