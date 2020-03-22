using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Newtonsoft.Json;

namespace ChatLib {
    public class LoginData {
        [JsonProperty( "UserName" )]
        [field: JsonIgnore]
        public string UserName { [DebuggerStepThrough] get; [DebuggerStepThrough] set; }

        [JsonProperty( "Password" )]
        [field: JsonIgnore]
        public string Password { [DebuggerStepThrough] get; [DebuggerStepThrough] set; }

        [JsonProperty( "Age" )]
        [field: JsonIgnore]
        public int Age { [DebuggerStepThrough] get; [DebuggerStepThrough] set; }

        [JsonProperty( "Gender" )]
        [field: JsonIgnore]
        public GenderTypes Gender { [DebuggerStepThrough] get; [DebuggerStepThrough] set; }

        public enum GenderTypes {
            MALE, FEMALE, DIVERS
        }

        public static LoginData CreateRegisterData(string userName, string password, int age, GenderTypes gender) {
            return new LoginData( userName, password, age, gender );
        }

        public static LoginData CreateLoginData(string userName, string password) {
            return new LoginData( userName, password );
        }

        private LoginData(string userName, string password, int age, GenderTypes gender) : this(userName, password){
            this.Age = age;
            this.Gender = gender;
        }

        private LoginData(string userName, string password) {
            this.UserName = userName;
            this.Password = password;
        }
    }
}
