package apiSharpServer.data;

import org.json.JSONObject;

public class Invite {
    private int inviteID;
    private String sender;
    private int senderID;
    private String receiver;
    private String chat;
    private int chatID;
    private String text;

    public void readFromJson(JSONObject in){
        setReceiver(in.getString("receiver"));
        setChatID(in.getInt("chatID"));
        setText(in.getString("text"));
    }

    public String getChat() {
        return chat;
    }

    public void setChat(String chat) {
        this.chat = chat;
    }

    public int getInviteID() {
        return inviteID;
    }

    public void setInviteID(int inviteID) {
        this.inviteID = inviteID;
    }

    public String getSender() {
        return sender;
    }

    public void setSender(String sender) {
        this.sender = sender;
    }

    public String getReceiver() {
        return receiver;
    }

    public void setReceiver(String receiver) {
        this.receiver = receiver;
    }

    public int getSenderID() {
        return senderID;
    }

    public void setSenderID(int senderID) {
        this.senderID = senderID;
    }

    public int getChatID() {
        return chatID;
    }

    public void setChatID(int chatID) {
        this.chatID = chatID;
    }

    public String getText() {
        return text;
    }

    public void setText(String text) {
        this.text = text;
    }
}
