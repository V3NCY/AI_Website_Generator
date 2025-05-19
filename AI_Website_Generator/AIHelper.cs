using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AI_Website_Generator
{
    public class AIHelper
    {
        private static readonly string apiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY");
        private static readonly string apiUrl = "https://api.openai.com/v1/chat/completions";

        public class OpenAIResponse
        {
            public Choice[] choices { get; set; }
        }

        public class Choice
        {
            public Message message { get; set; }
        }

        public class Message
        {
            public string content { get; set; }
        }

        public static async Task<(string, string)> GetAIStatusAndAction(string issueDescription)
        {
            if (string.IsNullOrEmpty(apiKey))
            {
                throw new InvalidOperationException("API Key is missing. Set OPENAI_API_KEY as an environment variable.");
            }

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

                var requestData = new
                {
                    model = "gpt-3.5-turbo",  // <- changed from gpt-4
                    messages = new[]
    {
        new { role = "system", content = "You are an AI assistant that analyzes website issues and suggests fixes." },
        new { role = "user", content = $"Analyze this issue and suggest a status and an action: {issueDescription}" }
    },
                    max_tokens = 150,
                    temperature = 0.6
                };


                string jsonContent = JsonConvert.SerializeObject(requestData);
                HttpContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(apiUrl, content);
                string responseString = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"OpenAI API Error: {responseString}");
                }

                OpenAIResponse responseObject = JsonConvert.DeserializeObject<OpenAIResponse>(responseString);

                if (responseObject?.choices == null || responseObject.choices.Length == 0 || string.IsNullOrEmpty(responseObject.choices[0].message.content))
                {
                    throw new Exception("Invalid response from OpenAI API.");
                }

                string[] aiResponseLines = responseObject.choices[0].message.content.Split('\n');
                string aiStatus = aiResponseLines.Length > 0 ? aiResponseLines[0] : "Unknown Status";
                string aiAction = aiResponseLines.Length > 1 ? aiResponseLines[1] : "No suggested action.";

                return (aiStatus, aiAction);
            }
        }
    }
}
