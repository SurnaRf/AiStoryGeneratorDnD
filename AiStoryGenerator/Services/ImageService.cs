using System.Text;
using System.Text.Json;

namespace AiStoryGenerator.Services;

public class ImageService
{
	private readonly HttpClient _httpClient = new HttpClient();

	private string apiKey = "INSERT_API_KEY";

	public async Task<string?> GenerateCharacterImage(string characterPrompt, string setting)
	{
		try
		{
			string prompt = $"{characterPrompt} in {setting}, fantasy RPG character, ultra detailed digital art";

			var requestBody = new
			{
				text_prompts = new[]
				{
					new { text = prompt }
				},
				cfg_scale = 7,
				height = 1024,
				width = 1024,
				samples = 1,
				steps = 50
			};

			string json = JsonSerializer.Serialize(requestBody);

			var request = new HttpRequestMessage(
				HttpMethod.Post,
				"https://api.stability.ai/v1/generation/stable-diffusion-xl-1024-v1-0/text-to-image"
			);

			request.Headers.Add("Authorization", $"Bearer {apiKey}");

			request.Content = new StringContent(json, Encoding.UTF8, "application/json");

			var response = await _httpClient.SendAsync(request);

			var responseString = await response.Content.ReadAsStringAsync();

			if (!response.IsSuccessStatusCode)
			{
				System.Diagnostics.Debug.WriteLine("STATUS: " + response.StatusCode);
				System.Diagnostics.Debug.WriteLine(responseString);
				return null;
			}

			using JsonDocument doc = JsonDocument.Parse(responseString);

			if (!doc.RootElement.TryGetProperty("artifacts", out JsonElement artifacts))
			{
				System.Diagnostics.Debug.WriteLine("No artifacts in response");
				return null;
			}

			if (artifacts.GetArrayLength() == 0)
			{
				System.Diagnostics.Debug.WriteLine("Empty artifacts array");
				return null;
			}

			var base64 = artifacts[0]
				.GetProperty("base64")
				.GetString();

			return base64;
		}
		catch (Exception ex)
		{
			System.Diagnostics.Debug.WriteLine("Exception: " + ex.Message);
			return null;
		}
	}
}