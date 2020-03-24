package tests;

import com.mysql.cj.xdevapi.JsonArray;
import org.json.JSONObject;

import java.util.HashMap;

public class LoginTest {
    String name;
    String password;
    int male;
    int age;

    public LoginTest(String name, String password, int male, int age) {
        this.name = name;
        this.password = password;
        this.male = male;
        this.age = age;
    }

    public LoginTest() {
    }

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

    public JSONObject packToJson(){
        JSONObject o = new JSONObject();
        o.put("name",name);
        o.put("password",password);
        o.put("male",male);
        o.put("age",age);
        return o;
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
