using System.Diagnostics;
using Newtonsoft.Json;

namespace ChatLib {
    public class Data {
        [JsonProperty( "Action" )]
        [field: JsonIgnore]
        public ActionEnum Action { [DebuggerStepThrough] get; [DebuggerStepThrough] set; }

        [JsonProperty( "Data" )]
        [field: JsonIgnore]
        public object DataObj { [DebuggerStepThrough] get; [DebuggerStepThrough] set; }

        [JsonProperty( "Token" )]
        [field: JsonIgnore]
        public string Token { [DebuggerStepThrough] get; [DebuggerStepThrough] set; }


        public Data(ActionEnum action, object dataObj, string token) {
            this.Action  = action;
            this.DataObj = dataObj;
            this.Token   = token;
        }

        public enum ActionEnum {
            CONNECTED      = 1,
            REGISTER       = 10, LOGIN             = 11,
            SEND_MESSAGE   = 20, GET_LAST_MESSAGES = 21,
            GET_LAST_CHATS = 30, GET_CHAT_INFO     = 31, CREATE_CHAT = 32, ADD_TO_CHAT = 33,


            SUCCEED                = 100,
            SUCCEED_LOGIN          = 110, SUCCEED_REGISTER          = 111,
            SUCCEED_MESSAGE_SEND   = 120, SUCCEED_GET_LAST_MESSAGES = 121,
            SUCCEED_GET_LAST_CHATS = 130, SUCCEED_GET_CHAT_INFO     = 131, SUCCEED_CREATE_CHAT = 132, SUCCEED_ADD_TO_CHAT = 133,
        }

        #region Overrides of Object

        /// <inheritdoc />
        public override string ToString() => nameof(this.Action) + ": " + this.Action + ", " + nameof(this.DataObj) + ": " + this.DataObj + ", " + nameof(this.Token) + ": " + this.Token;

        #endregion

    }
}
