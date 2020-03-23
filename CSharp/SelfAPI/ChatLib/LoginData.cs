using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Newtonsoft.Json;

namespace ChatLib {
    public class LoginData {
        [JsonProperty( "name" )]
        [field: JsonIgnore]
        public string Name { [DebuggerStepThrough] get; [DebuggerStepThrough] set; }

        [JsonProperty( "password" )]
        [field: JsonIgnore]
        public string Password { [DebuggerStepThrough] get; [DebuggerStepThrough] set; }

        [JsonProperty( "age" )]
        [field: JsonIgnore]
        public int Age { [DebuggerStepThrough] get; [DebuggerStepThrough] set; }

        [JsonProperty( "male" )]
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

        private LoginData(string name, string password, int age, GenderTypes gender) : this(name, password){
            this.Age = age;
            this.Gender = gender;
        }

        private LoginData(string name, string password) {
            this.Name = name;
            this.Password = password;
        }
    }
}
