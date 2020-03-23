package apiSharpServer;

import SqlConnection.Connection;
import apiSharpServer.data.Chat;
import apiSharpServer.data.Data;
import apiSharpServer.data.Login;
import apiSharpServer.data.Message;
import org.json.JSONObject;

import java.io.DataInputStream;
import java.io.DataOutputStream;
import java.io.IOException;
import java.net.ServerSocket;
import java.net.Socket;
import java.util.Date;
import java.util.LinkedList;
import java.util.List;
import java.util.concurrent.ExecutionException;

public class Main {

    private List<Thread> clients = new LinkedList<>();
    private Connection connectionDB = new Connection();
    private Security security = new Security();


    public Main() {
        startServer();
    }

    private void startServer() {
        try {
            ServerSocket server = new ServerSocket(6969);
            while (true) {
                System.out.println("Waiting for Client...");
                // wait.start();
                Socket client = server.accept();
                //wait.stop();
                System.out.printf("%nClient Connected: %s%n", client.getLocalAddress());

                newClientThread(client);
            }

        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    private void newClientThread(Socket client) {
        Thread clientThread = new Thread(() -> {
            try {
                DataInputStream in = new DataInputStream(client.getInputStream());
                DataOutputStream out = new DataOutputStream(client.getOutputStream());

                Data connected = new Data(ActionEnum.CONNECTED.getI(), null, null, null);

                out.write(connected.packToJson().toString().getBytes());

                listener(client, in, out);
                return;
            } catch (Exception e) {
                e.printStackTrace();
            }
        });
        clientThread.start();

        clients.add(clientThread);
    }

    private void listener(Socket client, DataInputStream in, DataOutputStream out) throws IOException, InterruptedException {
        StringBuilder stringIn;
        Data receive = new Data();

        System.out.println("Listener started...");
        while (true) {
            while (in.available() == 0) {

                Thread.sleep(10);
            }
            stringIn = new StringBuilder();
            receive = new Data();

            while (in.available() > 0) {
                stringIn.append((char) in.readByte());
            }

            receive.readFromJson(new JSONObject(stringIn.toString()));

            security.check(receive,connectionDB);

            int action = receive.getAction();
            if (action == ActionEnum.REGISTER.getI()) {
                register(out, receive);
            } else if (action == ActionEnum.LOGIN.getI()) {
                login(out, receive);
            } else if (action == ActionEnum.GET_LAST_CHATS.getI()) {
                lastChats(out, receive);
            } else if (action == ActionEnum.CREATE_CHAT.getI()) {
                createChat(out, receive);
            }else if(action == ActionEnum.ADD_TO_CHAT.getI()) {
                addUserToGroup(out, receive);
            }else if(action == ActionEnum.GET_LAST_MESSAGES.getI()){
                lastMessages(out, receive);
            }else if(action == ActionEnum.SEND_MESSAGE.getI()){
                addMessage(out, receive);
            }

            Thread.sleep(10);
        }
    }

    private void addMessage(DataOutputStream out, Data receive) throws IOException {
        Message m = receive.getMessages()[0];
        connectionDB.addMessage(m.getText(),m.getChat(),m.getSender(),m.getFlags());
        out.write(new Data(ActionEnum.SUCCEED_MESSAGE_SEND.getI()).packToJson().toString().getBytes());
    }

    private void lastMessages(DataOutputStream out, Data receive) throws IOException {
        Data d = new Data(ActionEnum.SUCCEED_GET_LAST_MESSAGES.getI());
        d.setMessages(connectionDB.getAllMessages((receive.getChats()[0].getChatID())));
        out.write(d.packToJson().toString().getBytes());
    }

    private void addUserToGroup(DataOutputStream out, Data receive) throws IOException {
        Chat c =receive.getChats()[0];
        if(connectionDB.moveIntoChat(receive.getLogin().getName(),c.getChatID(),c.getFlags())){
            out.write(new Data(ActionEnum.SUCCEED_ADD_TO_CHAT.getI()).packToJson().toString().getBytes());
            System.out.printf("Invite from %s to %s.%n",receive.getToken(),receive.getLogin().getName());
        }else{
            out.write(new Data(ActionEnum.ERROR_ADD_TO_CHAT.getI()).packToJson().toString().getBytes());
        }
    }

    private void createChat(DataOutputStream out, Data receive) throws IOException {
        Chat c = receive.getChats()[0];
        connectionDB.createChat(c.getCreatorID(), c.getTitle(), c.getFlags());
        out.write(new Data(ActionEnum.SUCCEED_CREATE_CHAT.getI()).packToJson().toString().getBytes());
    }

    private void lastChats(DataOutputStream out, Data receive) throws IOException {
        System.out.printf("Chat request from %s.%n", receive.getToken());
        Chat[] c = connectionDB.getAllChats(Integer.parseInt(receive.getToken()));
        out.write(new Data(ActionEnum.SUCCEED_GET_LAST_CHATS.getI(), c).packToJson().toString().getBytes());
    }

    private void login(DataOutputStream out, Data receive) throws IOException {
        Login reg = receive.getLogin();
        int usid = connectionDB.loginUser(reg.getName(), reg.getPassword());
        if (usid != -1) {
            out.write(new Data(ActionEnum.SUCCEED_LOGIN.getI(), null, null, String.valueOf(usid)/*Tokengen*/).packToJson().toString().getBytes());
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


    public static void main(String[] args) {
        new Main();
    }
}
//Start ma dein Client !!!!
