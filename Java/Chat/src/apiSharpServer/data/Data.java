package apiSharpServer.data;

import apiSharpServer.ActionEnum;
import org.json.JSONObject;

public class Data {
    public int action;
    public Message[] messages;
    public Login login;
    public String token;
    public Chat[] chats;


    public Data(int action, Message[] message, Login login, String token) {
        this.action = action;
        this.messages = message;
        this.login = login;
        this.token = token;
    }

    public Data(int action, Message[] messages, String token) {
        this.action = action;
        this.messages = messages;
        this.token = token;
    }

    public Data(int action, Chat[] chats) {
        this.action = action;
        this.chats = chats;
    }

    public Data(int action, Message[] messages, Login login, String token, Chat[] chats) {
        this.action = action;
        this.messages = messages;
        this.login = login;
        this.token = token;
        this.chats = chats;
    }

    public Data() {
    }

    public void readFromJson(JSONObject in) {
        setAction(in.getInt("action"));
        setToken(in.getString("token"));

        if ((action == ActionEnum.LOGIN.getI() || action == ActionEnum.REGISTER.getI()) && in.get("login") != null) {
            JSONObject t = (JSONObject) in.get("login");
            Login l = new Login();
            l.readFromJson(t);
            setLogin(l);
        } else if (in.get("messages") != null) {
            //TODO implent :)
        }

    }

    public JSONObject packToJson() {
        JSONObject out = new JSONObject();

        if (action == ActionEnum.LOGIN.getI() || action == ActionEnum.REGISTER.getI()) {
            out.put("login", login);
        } else if (action == ActionEnum.GET_LAST_MESSAGES.getI()) {
            out.put("messages", messages);
        } else if (action == ActionEnum.GET_LAST_CHATS.getI() || action == ActionEnum.CREATE_CHAT.getI()) {
            out.put("chats", chats);
        }

        out.put("action", action);
        out.put("token", token);

        return out;
    }

    public int getAction() {
        return action;
    }

    public void setAction(int action) {
        this.action = action;
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