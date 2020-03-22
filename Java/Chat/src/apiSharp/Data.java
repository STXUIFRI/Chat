package apiSharp;

import org.json.JSONArray;
import org.json.JSONObject;

public class Data {

    public String name = "Lol";
    public int age = 12;
    public int health = 100;
    public String[] list = {"hi","du"};


    public void readFromJson(JSONObject in){

        setName(in.getString("Name"));
        setAge(in.getInt("Age"));
        setHealth(in.getInt("Health"));

        JSONArray array = in.getJSONArray("List");
        String[] a = new String[array.length()];
        for (int i = 0; i < array.length(); i++) {
            a[i] = array.getString(i);
        }
        setList(a);

    }

    public JSONObject packToJson(){
        JSONObject out = new JSONObject();
        out.put("Name",name);
        out.put("Age", age);
        out.put("Health", health);
        out.put("list", list);
        return out;
    }

    public int getHealth() {
        return health;
    }

    public void setHealth(int health) {
        this.health = health;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public int getAge() {
        return age;
    }

    public void setAge(int age) {
        this.age = age;
    }

    public String[] getList() {
        return list;
    }

    public void setList(String[] list) {
        this.list = list;
    }
}
