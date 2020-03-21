package apiSharpServer;

public enum ActionEnum {
    REGISTER(10), LOGIN(11),
    SEND_MESSAGE(20), GET_LAST_MESSAGES(21),
    GET_LAST_CHATS(30), GET_CHAT_INFO(31), CREATE_CHAT(32), ADD_TO_CHAT(33),


    SUCCEED(100),
    SUCCEED_LOGIN(110), SUCCEED_REGISTER(111),
    SUCCEED_MESSAGE_SEND(120), SUCCEED_GET_LAST_MESSAGES(121),
    SUCCEED_GET_LAST_CHATS(130), SUCCEED_GET_CHAT_INFO(131), SUCCEED_CREATE_CHAT(132), SUCCEED_ADD_TO_CHAT(133);
    private int in;
    ActionEnum(int i) {
        in = i;
    }
    public int getI(){
        return in;
    }
}
