package tests;

import apiSharpServer.ActionEnum;
import apiSharpServer.data.Chat;
import apiSharpServer.data.Login;
import apiSharpServer.data.Message;
import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

public class DataTest {
    public int action;
    public Message[] messages;
    public LoginTest login;
    public String token;
    public Chat[] chats;
    public String errorMessage = null;

    public DataTest(int action) {
        this.action = action;
    }

    public DataTest(int action, Message[] message, LoginTest login, String token) {
        this.action = action;
        this.messages = message;
        this.login = login;
        this.token = token;
    }

    public DataTest(int action, Message[] messages, String token) {
        this.action = action;
        this.messages = messages;
        this.token = token;
    }

    public DataTest(int action, Chat[] chats) {
        this.action = action;
        this.chats = chats;
    }

    public DataTest(int action, LoginTest login, String token) {
        this.action = action;
        this.login = login;
        this.token = token;
    }

    public DataTest(int action, Message[] messages, LoginTest login, String token, Chat[] chats) {
        this.action = action;
        this.messages = messages;
        this.login = login;
        this.token = token;
        this.chats = chats;
    }

    public DataTest(int action, String token) {
        this.action = action;
        this.token = token;
    }

    public DataTest(int action, Message[] messages) {
        this.action = action;
        this.messages = messages;
    }



    public DataTest() {
    }



    public void readFromJson(JSONObject in) {
        try {
            setAction(in.getInt("action"));
            setToken(in.getString("token"));


            if ((action == ActionEnum.LOGIN.getI() || action == ActionEnum.REGISTER.getI() || action == ActionEnum.ADD_TO_CHAT.getI()) && in.get("login") != null) {
                JSONObject t = (JSONObject) in.get("login");
                LoginTest l = new LoginTest();
                l.readFromJson(t);
                setLogin(l);
            }
            if ((action == ActionEnum.SEND_MESSAGE.getI()) && in.get("messages") != null) {
                //TODO implent :)
                JSONArray array = in.getJSONArray("messages");
                messages = new Message[array.length()];
                for (int i = 0; i < array.length(); i++) {
                    Message newMessage = new Message();
                    newMessage.readFromJson(array.getJSONObject(i));
                    messages[i] = newMessage;
                }
            }
            if ((action == ActionEnum.GET_LAST_MESSAGES.getI() || action == ActionEnum.CREATE_CHAT.getI() || action == ActionEnum.ADD_TO_CHAT.getI()) && in.get("chats") != null) {
                JSONArray array = in.getJSONArray("chats");
                chats = new Chat[array.length()];
                for (int i = 0; i < array.length(); i++) {
                    Chat newChat = new Chat();
                    newChat.readFromJson(array.getJSONObject(i));
                    chats[i] = newChat;
                }
            }
        } catch (JSONException e) {
            action = ActionEnum.ERROR.getI();
            errorMessage = e.getMessage();
        }
    }

    public JSONObject packToJson() {
        JSONObject out = new JSONObject();

        if (action == ActionEnum.LOGIN.getI() || action == ActionEnum.REGISTER.getI()) {
            out.put("login", login.packToJson());
        } else if (action == ActionEnum.SUCCEED_GET_LAST_MESSAGES.getI()||action == ActionEnum.SEND_MESSAGE.getI()) { //Debug send message muss removed werden
            out.put("messages", messages);
        } else if (action == ActionEnum.SUCCEED_GET_LAST_CHATS.getI() || action == ActionEnum.CREATE_CHAT.getI()) {
            out.put("chats", chats);
        }

        if(errorMessage != null){
            out.put("ERROR", errorMessage);
        }
        out.put("action", action);
        out.put("token", token);

        return out;
    }

    public String getErrorMessage() {
        return errorMessage;
    }

    public void setErrorMessage(String errorMessage) {
        this.errorMessage = errorMessage;
    }

    public Message[] getMessages() {
        return messages;
    }

    public void setMessages(Message[] messages) {
        this.messages = messages;
    }

    public Chat[] getChats() {
        return chats;
    }

    public void setChats(Chat[] chats) {
        this.chats = chats;
    }

    public int getAction() {
        return action;
    }

    public void setAction(int action) {
        this.action = action;
    }

    public LoginTest getLogin() {
        return login;
    }

    public void setLogin(LoginTest login) {
        this.login = login;
    }

    public String getToken() {
        return token;
    }

    public void setToken(String token) {
        this.token = token;
    }
}