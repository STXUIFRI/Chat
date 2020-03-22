using System;
using System.Diagnostics;
using Newtonsoft.Json;

namespace ChatLib
{
   public class Message
    {
        [JsonProperty( "Flags" )]
        [field: JsonIgnore]
        public int Flags { [DebuggerStepThrough] get; [DebuggerStepThrough] set; }

        [JsonProperty( "Text" )]
        [field: JsonIgnore]
        public string Text { [DebuggerStepThrough] get; [DebuggerStepThrough] set; }

        [JsonProperty( "Sender" )]
        [field: JsonIgnore]
        public int Sender { [DebuggerStepThrough] get; [DebuggerStepThrough] set; }

        [JsonProperty( "Data" )]
        [field: JsonIgnore]
        public DateTime Data { [DebuggerStepThrough] get; [DebuggerStepThrough] set; }

        [JsonProperty( "Chat" )]
        [field: JsonIgnore]
        public int Chat { [DebuggerStepThrough] get; [DebuggerStepThrough] set; }


        public Message(string text, int sender, DateTime data, int chat) {
            this.Text = text;
            this.Sender = sender;
            this.Data = data;
            this.Chat = chat;
        }

        public Message(int flags, string text, int sender, DateTime data, int chat) {
            this.Flags = flags;
            this.Text = text;
            this.Sender = sender;
            this.Data = data;
            this.Chat = chat;
        }
    }
}
