using System;

namespace OpenAi
{
    [Serializable]
    public struct ChatCompletionResponse
    {
        public string id;
        public string _object;
        public long created;
        public string model;
        public Choice[] choices;
        public Usage usage;
        public string system_fingerprint;
    }

    [Serializable]
    public struct Choice
    {
        public int index;
        public Message message;
        public object logprobs;
        public string finish_reason;
    }

    [Serializable]
    public struct Usage
    {
        public int prompt_tokens;
        public int completion_tokens;
        public int total_tokens;
    }

    [Serializable]
    public struct Message
    {
        public string role;
        public string content;

        public Message(string role, string content)
        {
            this.role = role;
            this.content = content;
        }
    }
}
