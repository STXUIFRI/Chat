package apiSharpServer.data;

import org.json.JSONObject;

public class Chat {
    private int creatorID;
    private int chatID;
    private String title;
    private int flags;

    public Chat(int creatorID, int chatID, String title, int flags) {
        this.creatorID = creatorID;
        this.chatID = chatID;
        this.title = title;
        this.flags = flags;
    }

    public Chat() {
    }

    public void readFromJson(JSONObject in){
        setCreatorID(in.getInt("creatorID"));
        setTitle(in.getString("title"));
        setFlags(in.getInt("flags"));
        setChatID(in.getInt("chatID"));
    }

    public int getChatID() {
        return chatID;
    }

    public void setChatID(int chatID) {
        this.chatID = chatID;
    }

    public int getCreatorID() {
        return creatorID;
    }

    public void setCreatorID(int creatorID) {
        this.creatorID = creatorID;
    }

    public String getTitle() {
        return title;
    }

    public void setTitle(String title) {
        this.title = title;
    }

    public int getFlags() {
        return flags;
    }

    public void setFlags(int flags) {
        this.flags = flags;
    }
}
