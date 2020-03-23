package apiSharpServer.data;

import org.json.JSONObject;

public class Message {
    private String text;
    private String date;
    private int sender;
    private int chat;
    private int flags;

    public Message() {
    }

    public Message(String text, String date, int sender, int chat, int flags) {
        this.text = text;
        this.date = date;
        this.sender = sender;
        this.chat = chat;
        this.flags = flags;
    }

    public void readFromJson(JSONObject in){
        setText(in.getString("text"));
        setSender(in.getInt("sender"));
        setChat(in.getInt("chat"));
        setFlags(in.getInt("flags"));
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

    public int getSender() {
        return sender;
    }

    public void setSender(int sender) {
        this.sender = sender;
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
