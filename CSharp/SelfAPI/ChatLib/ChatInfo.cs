using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Newtonsoft.Json;

namespace ChatLib {
    public class ChatInfo {

        [JsonProperty( "cID" )]
        [field: JsonIgnore]
        public int ChatId { [DebuggerStepThrough] get; [DebuggerStepThrough] set; }


        [JsonProperty( "creator" )]
        [field: JsonIgnore]
        public int Creator { [DebuggerStepThrough] get; [DebuggerStepThrough] set; }

        [JsonProperty( "flags" )]
        [field: JsonIgnore]
        public int Flags { [DebuggerStepThrough] get; [DebuggerStepThrough] set; }

        [JsonProperty( "title" )]
        [field: JsonIgnore]
        public string Title { [DebuggerStepThrough] get; [DebuggerStepThrough] set; }

        public ChatInfo(int chatId, int creator, int flags, string title) {
            this.ChatId = chatId;
            this.Creator = creator;
            this.Flags = flags;
            this.Title = title;
        }
    }
}
