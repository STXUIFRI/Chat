using System.Diagnostics;
using Newtonsoft.Json;

namespace APTtoJava {
    public class Data {

        #region Overrides of Object

        /// <inheritdoc />
        public override string ToString() { return $"{nameof(this.Name)}: {this.Name}, {nameof(this.Age)}: {this.Age}, {nameof(this.Health)}: {this.Health}"; }

        #endregion

        public Data(int health, string name, int age, MethodOptions method = MethodOptions.POST, ErrorOptions error = ErrorOptions.ERROR_NONE, ResponseOptions response = ResponseOptions.OK) {
            this.Health   = health;
            this.Name     = name;
            this.Method   = method;
            this.Age      = age;
            this.Error    = error;
            this.Response = response;
            this.List     = new[] { "", "" };
        }

        [JsonProperty( "Health" )]
        [field: JsonIgnore]
        public int Health { [DebuggerStepThrough] get; [DebuggerStepThrough] set; }

        [JsonProperty( "Name" )]
        [field: JsonIgnore]
        public string Name { [DebuggerStepThrough] get; [DebuggerStepThrough] set; }

        [JsonProperty( "Method" )]
        [field: JsonIgnore]
        public MethodOptions Method { [DebuggerStepThrough] get; [DebuggerStepThrough] set; }

        [JsonProperty( "Age" )]
        [field: JsonIgnore]
        public int Age { [DebuggerStepThrough] get; [DebuggerStepThrough] set; }

        [JsonProperty( "List" )]
        [field: JsonIgnore]
        public string[] List { [DebuggerStepThrough] get; [DebuggerStepThrough] set; }

        [JsonProperty( "Error" )]
        [field: JsonIgnore]
        public ErrorOptions Error { [DebuggerStepThrough] get; [DebuggerStepThrough] set; }

        [JsonProperty( "Response" )]
        [field: JsonIgnore]
        public ResponseOptions Response { [DebuggerStepThrough] get; [DebuggerStepThrough] set; }

        public enum MethodOptions {
            GET = 0, POST = 1, PUT = 2,
        }

        public enum ErrorOptions {
            ERROR_NONE           = 0,
            UNKNOWN_ERROR        = -1,
            ACTION_NOT_PERMITTED = 11,
            INTERNAL_ERROR       = 12,
        }

        public enum ResponseOptions {
            OK = 0, ERROR = -1, E_404 = 404, E_503 = 503
        }
    }
}