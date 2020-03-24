using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Newtonsoft.Json;

namespace ChatLib {
    public class InviteInfo {
        [JsonProperty( "chat" )]
        [field: JsonIgnore]
        public string ChatName { [DebuggerStepThrough] get; [DebuggerStepThrough] set; }

        [JsonProperty( "chatID" )]
        [field: JsonIgnore]
        public int ChatID { [DebuggerStepThrough] get; [DebuggerStepThrough] set; }

        [JsonProperty( "sender" )]
        [field: JsonIgnore]
        public string Sender { [DebuggerStepThrough] get; [DebuggerStepThrough] set; }

        [JsonProperty( "receiver" )]
        [field: JsonIgnore]
        public string Receiver { [DebuggerStepThrough] get; [DebuggerStepThrough] set; }

        [JsonProperty( "text" )]
        [field: JsonIgnore]
        public string Text { [DebuggerStepThrough] get; [DebuggerStepThrough] set; }

        [JsonProperty( "inviteID" )]
        [field: JsonIgnore]
        public int InviteID { [DebuggerStepThrough] get; [DebuggerStepThrough] set; }

        public InviteInfo() { }

        public InviteInfo(int inviteId) => this.InviteID = inviteId;

        public InviteInfo(int chatId, string receiver, string text) {
            this.ChatID   = chatId;
            this.Receiver = receiver;
            this.Text     = text;
        }

    }
}
