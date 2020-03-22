package apiSharpServer.data;

public class Chat {
    private int cID;
    private String title;
    private int flags;

    public Chat(int cID, String title, int flags) {
        this.cID = cID;
        this.title = title;
        this.flags = flags;
    }
}
