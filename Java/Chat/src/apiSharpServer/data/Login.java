package apiSharpServer.data;

import org.json.JSONObject;

import java.util.HashMap;

public class Login {
    String name;
    String password;
    int male;
    int age;

    public void readFromHashMap(HashMap<String, Object> in){
        setName((String)in.get("name"));
        setPassword((String)in.get("password"));
        setAge((Integer)in.get("age"));
        setMale((Integer)in.get("male"));
    }

    public void readFromJson(JSONObject in){
        setName(in.getString("name"));
        setPassword(in.getString("password"));
        setMale(in.getInt("male"));
        setAge(in.getInt("age"));
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public String getPassword() {
        return password;
    }

    public void setPassword(String password) {
        this.password = password;
    }

    public int getMale() {
        return male;
    }

    public void setMale(int male) {
        this.male = male;
    }

    public int getAge() {
        return age;
    }

    public void setAge(int age) {
        this.age = age;
    }
}
