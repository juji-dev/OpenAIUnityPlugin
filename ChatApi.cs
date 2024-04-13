using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace OpenAi
{
    public class ChatApi
    { 
        public delegate void OpenAiResponseEventHandler(string message);
        public static event OpenAiResponseEventHandler? OnOpenAiResponseReceived;

        private static readonly string apiEndPoint = "https://api.openai.com/v1/chat/completions";
        private static readonly string openAiModel = "gpt-3.5-turbo";
        private static readonly string apiKey = "YOUR_OPEN_AI_API_KEY";
        private static readonly string contentType = "application/json";
        private static readonly string authorizationPrefix = "Bearer ";

        public static IEnumerator PostOpenAiRequest(Message message)
        {
            string requestBodyJsonStr = "{\"model\": \"" + openAiModel +
                                        "\", \"messages\": [{\"role\": \"" +
                                        message.role + "\", \"content\": \"" +
                                        message.content + "\"}]}";

            UnityWebRequest request = new UnityWebRequest(apiEndPoint, "POST");

            request.SetRequestHeader("Content-Type", contentType);
            request.SetRequestHeader("Authorization", authorizationPrefix + apiKey);

            byte[] bodyRaw = new System.Text.UTF8Encoding().GetBytes(requestBodyJsonStr);
            request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();

            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error: " + request.error);
            }
            else
            {
                ChatCompletionResponse responseMessage = JsonUtility.FromJson<ChatCompletionResponse>(request.downloadHandler.text);
                OnOpenAiResponseReceived?.Invoke(responseMessage.choices[0].message.content);
            }
        }
    }
}
