package apiSharpServer.data;

import org.json.JSONObject;

public class Message {
    private String text;
    private String date;
    private String sender;
    private int chat;
    private int flags;

    public Message() {
    }

    public Message(String text, String date,String sender ,int chat, int flags) {
        this.text = text;
        this.date = date;
        this.sender = sender;
        this.chat = chat;
        this.flags = flags;
    }

    public void readFromJson(JSONObject in){
        setText(in.getString("text"));
        //setSender(in.getString("sender"));
        setChat(in.getInt("chat"));
        setFlags(in.getInt("flags"));
    }

    public String getSender() {
        return sender;
    }

    public void setSender(String sender) {
        this.sender = sender;
    }

    public String getDate() {
        return date;
    }

    public void setDate(String date) {
        this.date = date;
    }

    public String getText() {
        return text;
    }

    public void setText(String text) {
        this.text = text;
    }


    public int getChat() {
        return chat;
    }

    public void setChat(int chat) {
        this.chat = chat;
    }

    public int getFlags() {
        return flags;
    }

    public void setFlags(int flags) {
        this.flags = flags;
    }
}
