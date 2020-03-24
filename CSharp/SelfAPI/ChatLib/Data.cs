#region using

using System.Diagnostics;
using Newtonsoft.Json;

#endregion

namespace ChatLib {
    public class Data {

        public enum ActionEnum {
            CONNECTED      = 1,
            REGISTER       = 10, LOGIN             = 11,
            SEND_MESSAGE   = 20, GET_LAST_MESSAGES = 21,
            GET_LAST_CHATS = 30, GET_CHAT_INFO     = 31, CREATE_CHAT = 32, ADD_TO_CHAT = 33,
            GET_INVITES    = 40, ACCEPT_INVITE     = 41, DENY_INVITE = 42,


            SUCCEED                = 100,
            SUCCEED_LOGIN          = 110, SUCCEED_REGISTER          = 111,
            SUCCEED_MESSAGE_SEND   = 120, SUCCEED_GET_LAST_MESSAGES = 121,
            SUCCEED_GET_LAST_CHATS = 130, SUCCEED_GET_CHAT_INFO     = 131, SUCCEED_CREATE_CHAT = 132, SUCCEED_ADD_TO_CHAT = 133,
            SUCCEED_GET_INVITES    = 140, SUCCEED_ACCEPT_INVITE     = 141, SUCCEED_DENY_INVITE = 142,


            ERROR                = 200, ERROR_SQL_INJECTION_DETECTED = 201,
            ERROR_LOGIN          = 210, ERROR_REGISTER               = 211,
            ERROR_MESSAGE_SEND   = 220, ERROR_GET_LAST_MESSAGES      = 221,
            ERROR_GET_LAST_CHATS = 230, ERROR_GET_CHAT_INFO          = 231, ERROR_CREATE_CHAT = 232, ERROR_ADD_TO_CHAT = 233,
            ERROR_GET_INVITES    = 240, ERROR_ACCEPT_INVITE          = 241, ERROR_DENY_INVITE = 242,

            ERROR_CLIENT_CLOSED = -100
        }


        public Data() { }

        [DebuggerStepThrough]
        public Data(ActionEnum action, string token = "-") {
            this.Action = action;
            this.Token  = token;
        }

        [DebuggerStepThrough]
        private Data(ActionEnum action, ChatInfo[] chats = default) : this( action, default, default, chats ) { }

        [DebuggerStepThrough]
        private Data(ActionEnum action, InviteInfo[] invites = default) : this( action, default, default, default, invites ) { }

        [DebuggerStepThrough]
        private Data(ActionEnum action, Message[] messages = default) : this( action, default, messages ) { }

        [DebuggerStepThrough]
        private Data(ActionEnum action, LoginData login = default, Message[] messages = default, ChatInfo[] chats = default, InviteInfo[] invites = default, string token = "-") {
            this.Action   = action;
            this.Login    = login;
            this.Messages = messages;
            this.Invites  = invites;
            this.Chats    = chats;
            this.Token    = token;
        }

        private Data(ActionEnum action) => this.Action = action;

        [JsonProperty( "action" )]
        [field: JsonIgnore]
        public ActionEnum Action { [DebuggerStepThrough] get; [DebuggerStepThrough] set; }

        [JsonProperty( "login" )]
        [field: JsonIgnore]
        public LoginData Login { [DebuggerStepThrough] get; [DebuggerStepThrough] set; }

        [JsonProperty( "messages" )]
        [field: JsonIgnore]
        public Message[] Messages { [DebuggerStepThrough] get; [DebuggerStepThrough] set; }

        [JsonProperty( "chats" )]
        [field: JsonIgnore]
        public ChatInfo[] Chats { [DebuggerStepThrough] get; [DebuggerStepThrough] set; }

        [JsonProperty( "invites" )]
        [field: JsonIgnore]
        public InviteInfo[] Invites { [DebuggerStepThrough] get; [DebuggerStepThrough] set; }

        [JsonProperty( "token" )]
        [field: JsonIgnore]
        public string Token { [DebuggerStepThrough] get; [DebuggerStepThrough] set; }

        [JsonProperty( "Error" )]
        [field: JsonIgnore]
        public string Error { [DebuggerStepThrough] get; [DebuggerStepThrough] set; }


        public static Data CreateInvite(InviteInfo  chatToInvite) { return new Data( ActionEnum.ADD_TO_CHAT,       new[] { chatToInvite } ); }
        public static Data CreateChat(ChatInfo      chatToCreate) { return new Data( ActionEnum.CREATE_CHAT,       new[] { chatToCreate } ); }
        public static Data GetLastMessages(ChatInfo chatSelector) { return new Data( ActionEnum.GET_LAST_MESSAGES, new[] { chatSelector } ); }
        public static Data SendMessage(Message      msg)          { return new Data( ActionEnum.SEND_MESSAGE,      new[] { msg } ); }
        public static Data CreateLogin(LoginData    loginData)    => new Data( ActionEnum.LOGIN,    loginData );
        public static Data CreateRegister(LoginData registerData) => new Data( ActionEnum.REGISTER, registerData );

        #region Overrides of Object

        /// <inheritdoc />
        public string ToString(bool all) => nameof(this.Action) + ": " + this.Action + ", " + nameof(this.Messages) + ": " + this.Messages.Length + ", " + nameof(this.Chats) + ": " + this.Chats.Length + ", " + nameof(this.Login) + ": " + this.Login + ", " + nameof(this.Token) + ": " + this.Token;

        /// <inheritdoc />
        public override string ToString() => nameof(this.Action) + ": " + this.Action;

        #endregion

        public static Data GetInvites()        { return new Data( ActionEnum.GET_INVITES ); }
        public static Data DenyInvite(int   i) { return new Data( ActionEnum.DENY_INVITE,   new[] { new InviteInfo( i ) } ); }
        public static Data AcceptInvite(int i) { return new Data( ActionEnum.ACCEPT_INVITE, new[] { new InviteInfo( i ) } ); }
    }
}
