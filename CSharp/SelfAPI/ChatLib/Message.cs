#region using

using System.Diagnostics;
using Newtonsoft.Json;

#endregion

namespace ChatLib {
    public class Message {
        public Message(string text, int sender, int chat) {
            this.Text   = text;
            this.Sender = sender;
            this.Chat   = chat;
            this.Flags  = 0;
        }

        public Message() { }

        public Message(string text, int sender, int chat, int flags) {
            this.Flags  = flags;
            this.Text   = text;
            this.Sender = sender;
            this.Chat   = chat;
        }


        [JsonProperty( "text" )]
        [field: JsonIgnore]
        public string Text { [DebuggerStepThrough] get; [DebuggerStepThrough] set; }

        [JsonProperty( "sender" )]
        [field: JsonIgnore]
        public int Sender { [DebuggerStepThrough] get; [DebuggerStepThrough] set; }

        [JsonProperty( "chat" )]
        [field: JsonIgnore]
        public int Chat { [DebuggerStepThrough] get; [DebuggerStepThrough] set; }

        [JsonProperty( "flags" )]
        [field: JsonIgnore]
        public int Flags { [DebuggerStepThrough] get; [DebuggerStepThrough] set; }
    }
}
