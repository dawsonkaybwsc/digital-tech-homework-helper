using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace HomeworkHelper
{

    public record OpenRouterRequest(
        [property: JsonPropertyName("model")] string Model,
        [property: JsonPropertyName("messages")] OpenRouterMessage[] Messages
    );

    public record OpenRouterMessage(
        [property: JsonPropertyName("role")] string Role,
        [property: JsonPropertyName("content")] string Content
    );

    public record OpenRouterChoice(
        [property: JsonPropertyName("message")] OpenRouterMessage Message
    );

    public record OpenRouterResponse(
        [property: JsonPropertyName("choices")] OpenRouterChoice[] Choices
    );

    public class OpenRouterApi
    {
        private static readonly HttpClient _httpClient = new HttpClient();
        private const string ApiKey = "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";
        private const string ApiUrl = "https://openrouter.ai/api/v1/chat/completions";

        public async Task<string> GetCompletionAsync(string prompt, string model)
        {
            var requestData = new OpenRouterRequest(
                Model: model,
                Messages: messages.ToArray()
            );

            using var request = new HttpRequestMessage(HttpMethod.Post, ApiUrl);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", ApiKey);
            request.Headers.Add("HTTP-Referer", "https://github.com/dawsonkaybwsc/digital-tech-homework-helper");
            request.Headers.Add("X-Title", "Homework Helper");

            request.Content = JsonContent.Create(requestData);

            try
            {
                var response = await _httpClient.SendAsync(request);
                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    return $"API Error (Status {(int)response.StatusCode}): {errorContent}";
                }

                var result = await response.Content.ReadFromJsonAsync<OpenRouterResponse>();
                if (result?.Choices != null && result.Choices.Length > 0)
                {
                    return result.Choices[0].Message.Content;
                }

                return "Error recieved. From OpenRouter";
            }
            catch (HttpRequestException ex)
            {
                return $"Network error: {ex.Message}";
            }
            catch (Exception ex)
            {
                return $"Unexpected error: {ex.Message}";
            }
        }
    }
}
