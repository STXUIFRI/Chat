package apiSharpServer;

import SqlConnection.Connection;
import apiSharpServer.data.*;
import org.json.JSONObject;

import java.io.ByteArrayOutputStream;
import java.io.DataInputStream;
import java.io.DataOutputStream;
import java.io.IOException;
import java.net.Socket;
import java.nio.charset.Charset;
import java.util.ArrayList;
import java.util.List;

public class Client {
    private Socket connect;
    private Connection connectionDB;
    private Security security;

    private int ID;
    private String tokenName;

    private DataInputStream in;
    private DataOutputStream out;

    public Client(Socket connect, Connection connectionDB, Security security) {
        this.connect = connect;
        this.connectionDB = connectionDB;
        this.security = security;

        init();
    }

    private void init() {
        try {
            openStreams();
            sendConnectionPacket();

        } catch (Exception e) {
            e.printStackTrace();
        }

    }

    public void listener() throws IOException, InterruptedException {
        if (in.available() != 0) {
            try {
                Data receive = new Data();
                String stringIn = readLine(in);

                System.out.printf("Received: %s%n",stringIn);
                receive.readFromJson(new JSONObject(stringIn));

                if (security.check(receive, connectionDB, tokenName)) {

                    int action = receive.getAction();
                    if (action == ActionEnum.ERROR.getI()) {
                        out.write(receive.packToJson().toString().getBytes());
                    } else if (action == ActionEnum.REGISTER.getI()) {
                        register(out, receive);
                    } else if (action == ActionEnum.LOGIN.getI()) {
                        login(out, receive);
                    } else if (action == ActionEnum.GET_LAST_CHATS.getI()) {
                        lastChats(out, receive);
                    } else if (action == ActionEnum.CREATE_CHAT.getI()) {
                        createChat(out, receive);
                    } else if (action == ActionEnum.GET_LAST_MESSAGES.getI()) {
                        lastMessages(out, receive);
                    } else if (action == ActionEnum.SEND_MESSAGE.getI()) {
                        addMessage(out, receive);
                    }else if(action == ActionEnum.GET_INVITES.getI()){
                        Data d = new Data(ActionEnum.SUCCEED_GET_INVITES.getI());
                        d.setInvites(connectionDB.getAllInvites(ID));
                        out.write(d.packToJson().toString().getBytes());
                    }else if(action == ActionEnum.ADD_TO_CHAT.getI()){
                        Invite i =receive.getInvites()[0];
                        if(connectionDB.addInvite(ID,i.getReceiver(),i.getChatID(),i.getText())){
                            out.write(new Data(ActionEnum.SUCCEED_ADD_TO_CHAT.getI()).packToJson().toString().getBytes());
                        }else{
                            out.write(new Data(ActionEnum.ERROR_ADD_TO_CHAT.getI()).packToJson().toString().getBytes());

                        }
                    }
                }else{
                    Data d = new Data(ActionEnum.ERROR.getI());
                    d.setErrorMessage("Access Denied.");
                    out.write(d.packToJson().toString().getBytes());
                    System.err.println("Untrusted access!");
                }
            } catch (Exception e) {
                e.printStackTrace();
            }

        }
    }

    private void addMessage(DataOutputStream out, Data receive) throws IOException {
        Message m = receive.getMessages()[0];
        connectionDB.addMessage(m.getText(), m.getChat(), ID, m.getFlags());
        out.write(new Data(ActionEnum.SUCCEED_MESSAGE_SEND.getI()).packToJson().toString().getBytes());
    }

    private void lastMessages(DataOutputStream out, Data receive) throws IOException {
        Data d = new Data(ActionEnum.SUCCEED_GET_LAST_MESSAGES.getI());
        d.setMessages(connectionDB.getAllMessages((receive.getChats()[0].getChatID())));
        out.write(d.packToJson().toString().getBytes());
    }

    private void addUserToGroup(DataOutputStream out, Data receive) throws IOException {
        Chat c = receive.getChats()[0];
        if (connectionDB.moveIntoChat(receive.getLogin().getName(), c.getChatID(), c.getFlags())) {
            out.write(new Data(ActionEnum.SUCCEED_ADD_TO_CHAT.getI()).packToJson().toString().getBytes());
            System.out.printf("Invite from %s to %s.%n", receive.getToken(), receive.getLogin().getName());
        } else {
            out.write(new Data(ActionEnum.ERROR_ADD_TO_CHAT.getI()).packToJson().toString().getBytes());
        }
    }

    private void createChat(DataOutputStream out, Data receive) throws IOException {
        Chat c = receive.getChats()[0];
        connectionDB.createChat(ID, c.getTitle(), c.getFlags());
        out.write(new Data(ActionEnum.SUCCEED_CREATE_CHAT.getI()).packToJson().toString().getBytes());
    }

    private void lastChats(DataOutputStream out, Data receive) throws IOException {
        System.out.printf("Chat request from %s.%n", receive.getToken());
        Chat[] c = connectionDB.getAllChats(ID); // token statt
        out.write(new Data(ActionEnum.SUCCEED_GET_LAST_CHATS.getI(), c).packToJson().toString().getBytes());
    }

    private void login(DataOutputStream out, Data receive) throws IOException {
        Login reg = receive.getLogin();
        int usid = connectionDB.loginUser(reg.getName(), reg.getPassword());
        ID = usid;
        if (usid != -1) {
            out.write(new Data(ActionEnum.SUCCEED_LOGIN.getI(), null, null, String.valueOf(tokenName)/*Tokengen*/).packToJson().toString().getBytes());
            System.out.printf("%s logged in.%n", reg.getName());
        } else {
            out.write(new Data(ActionEnum.ERROR_LOGIN.getI(), null, null, null).packToJson().toString().getBytes());
        }
    }

    private void register(DataOutputStream out, Data receive) throws IOException {
        Login reg = receive.getLogin();
        if (connectionDB.registerUser(reg.getName(), reg.getPassword(), reg.getMale(), reg.getAge())) {

            out.write(new Data(ActionEnum.SUCCEED_REGISTER.getI(), null, null, null /*Tokengen*/).packToJson().toString().getBytes());
            System.out.printf("%s has registered.%n", reg.getName());
        } else {
            out.write(new Data(ActionEnum.ERROR_REGISTER.getI(), null, null, null).packToJson().toString().getBytes());
        }
    }

    public  String readLine(DataInputStream in) throws IOException {
        ByteArrayOutputStream buffer = new ByteArrayOutputStream();
        while (true) {
            int b = in.read();
            if (b < 0) {
                throw new IOException("Data truncated");
            }
            if (b == 0x0A) {
                break;
            }
            buffer.write(b);
        }
        return new String(buffer.toByteArray(), "UTF-8");
    }

    private void sendConnectionPacket() throws IOException {
        Data connected = new Data(ActionEnum.CONNECTED.getI(), null, null, null);
        out.write(connected.packToJson().toString().getBytes());
    }

    private void openStreams() throws IOException {
        in = new DataInputStream(connect.getInputStream());
        out = new DataOutputStream(connect.getOutputStream());
    }

    public Socket getConnect() {
        return connect;
    }

    public void setConnect(Socket connect) {
        this.connect = connect;
    }

    public Connection getConnectionDB() {
        return connectionDB;
    }

    public void setConnectionDB(Connection connectionDB) {
        this.connectionDB = connectionDB;
    }

    public Security getSecurity() {
        return security;
    }

    public void setSecurity(Security security) {
        this.security = security;
    }

    public DataInputStream getIn() {
        return in;
    }

    public void setIn(DataInputStream in) {
        this.in = in;
    }

    public DataOutputStream getOut() {
        return out;
    }

    public void setOut(DataOutputStream out) {
        this.out = out;
    }

    public int getID() {
        return ID;
    }

    public void setID(int ID) {
        this.ID = ID;
    }

    public String getTokenName() {
        return tokenName;
    }

    public void setTokenName(String tokenName) {
        this.tokenName = tokenName;
    }
}
