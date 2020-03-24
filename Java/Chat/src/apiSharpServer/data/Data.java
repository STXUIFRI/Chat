package apiSharpServer.data;

import apiSharpServer.ActionEnum;
import com.mysql.cj.xdevapi.JsonArray;
import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import javax.swing.*;

public class Data {
    public int action;
    public Message[] messages;
    public Login login;
    public String token;
    public Chat[] chats;
    public Invite[] invites;
    public String errorMessage = null;

    public Data(int action) {
        this.action = action;
    }

    public Data(int action, Message[] message, Login login, String token) {
        this.action = action;
        this.messages = message;
        this.login = login;
        this.token = token;
    }

    public Data(int action, Chat[] chats) {
        this.action = action;
        this.chats = chats;
    }

    public Data() {
    }

    public void readFromJson(JSONObject in) {
        try {
            setAction(in.getInt("action"));
            setToken(in.getString("token"));


            if ((action == ActionEnum.LOGIN.getI() || action == ActionEnum.REGISTER.getI()&& in.get("login") != null)) {
                JSONObject t = (JSONObject) in.get("login");
                Login l = new Login();
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
            if ((action == ActionEnum.GET_LAST_MESSAGES.getI() || action == ActionEnum.CREATE_CHAT.getI()) && in.get("chats") != null) {
                JSONArray array = in.getJSONArray("chats");
                chats = new Chat[array.length()];
                for (int i = 0; i < array.length(); i++) {
                    Chat newChat = new Chat();
                    newChat.readFromJson(array.getJSONObject(i));
                    chats[i] = newChat;
                }
            }
            if((action == ActionEnum.ADD_TO_CHAT.getI())&&in.get("invites") != null){
                JSONArray array = in.getJSONArray("invites");
                invites = new Invite[array.length()];
                for (int i = 0; i < array.length(); i++) {
                    Invite newIN = new Invite();
                    newIN.readFromJson(array.getJSONObject(i));
                    invites[i] = newIN;
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
            out.put("login", login);
        } else if (action == ActionEnum.SUCCEED_GET_LAST_MESSAGES.getI()) {
            out.put("messages", messages);
        } else if (action == ActionEnum.SUCCEED_GET_LAST_CHATS.getI() || action == ActionEnum.CREATE_CHAT.getI()) {
            out.put("chats", chats);
        }else if(action == ActionEnum.SUCCEED_GET_INVITES.getI()){
            out.put("invites",invites);
        }

        if(errorMessage != null){
            out.put("ERROR", errorMessage);
        }
        out.put("action", action);
        out.put("token", token);

        return out;
    }

    public Invite[] getInvites() {
        return invites;
    }

    public void setInvites(Invite[] invites) {
        this.invites = invites;
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