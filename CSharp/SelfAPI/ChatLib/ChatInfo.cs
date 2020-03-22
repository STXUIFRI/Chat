using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Newtonsoft.Json;

namespace ChatLib {
    public class ChatInfo {

        [JsonProperty( "ChatId" )]
        [field: JsonIgnore]
        public int ChatId { [DebuggerStepThrough] get; [DebuggerStepThrough] set; }


        [JsonProperty( "Creator" )]
        [field: JsonIgnore]
        public int Creator { [DebuggerStepThrough] get; [DebuggerStepThrough] set; }

        [JsonProperty( "Flags" )]
        [field: JsonIgnore]
        public int Flags { [DebuggerStepThrough] get; [DebuggerStepThrough] set; }

        public ChatInfo(int chatId, int creator, int flags) {
            this.ChatId = chatId;
            this.Creator = creator;
            this.Flags = flags;
        }
    }
}
