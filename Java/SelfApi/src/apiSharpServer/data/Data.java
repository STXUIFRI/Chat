package apiSharpServer.data;

import apiSharpServer.ActionEnum;
import com.mysql.cj.log.Log;
import com.mysql.cj.xdevapi.JsonArray;
import org.json.JSONObject;

import java.util.UUID;

public class Data {
    public int action;
    public Message message;
    public Login login;
    public String token;


    public Data() {

    }
    public void readFromJson(JSONObject in){
        setAction(in.getInt("action"));
        if(action == ActionEnum.LOGIN.getI()||action == ActionEnum.REGISTER.getI()){
            setLogin((Login)in.get("login"));
        }else{
            setMessage((Message) in.get("message"));
        }
        setToken(in.getString("token"));
    }

    public JSONObject packToJson(){
        JSONObject out = new JSONObject();
        out.put("action", action);
        if(action == ActionEnum.LOGIN.getI()||action == ActionEnum.REGISTER.getI()){
            out.put("login", login);
        }else{
            out.put("message", message);
        }
        out.put("token", token);
    }

    public int getAction() {
        return action;
    }

    public void setAction(int action) {
        this.action = action;
    }

    public Message getMessage() {
        return message;
    }

    public void setMessage(Message message) {
        this.message = message;
    }

    public Login getLogin() {
        return login;
    }

    public void setLogin(Login login) {
        this.login = login;
    }

    public String getToken() {
        return token;
    }

    public void setToken(String token) {
        this.token = token;
    }
}
