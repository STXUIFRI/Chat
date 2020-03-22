package apiSharpServer;

import SqlConnection.Connection;
import apiSharpServer.data.Chat;
import apiSharpServer.data.Data;
import apiSharpServer.data.Login;
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

    private  Thread wait = new Thread(()->{
        while (true) {
            System.out.print(".");
            try {
                Thread.sleep(1000);
            } catch (InterruptedException e) {
                e.printStackTrace();
            }
        }
    });

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

    private void newClientThread(Socket client){
        Thread clientThread = new Thread(()->{
            try {
                DataInputStream in = new DataInputStream(client.getInputStream());
                DataOutputStream out = new DataOutputStream(client.getOutputStream());

                Data connected = new Data(ActionEnum.CONNECTED.getI(),null,null,null);

                out.write(connected.packToJson().toString().getBytes());

                listener(client, in,out);
                return;
            }catch (Exception e){
                e.printStackTrace();
            }
        });
        clientThread.start();

        clients.add(clientThread);
    }

    private void listener(Socket client, DataInputStream in,DataOutputStream out) throws IOException, InterruptedException {
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

            if(receive.getAction() == ActionEnum.REGISTER.getI()){
                Login reg = receive.getLogin();
               if( connectionDB.registerUser(reg.getName(),reg.getPassword(),reg.getMale(),reg.getAge())){

                   out.write(new Data(ActionEnum.SUCCEED_REGISTER.getI(),null,null,null /*Tokengen*/).packToJson().toString().getBytes());
               }else{
                   out.write(new Data(ActionEnum.ERROR_REGISTER.getI(),null,null,null).packToJson().toString().getBytes());
               }
            }else if(receive.getAction() == ActionEnum.LOGIN.getI()) {
                Login reg = receive.getLogin();
                int usid = connectionDB.loginUser(reg.getName(),reg.getPassword());
                if(usid != -1){
                    out.write(new Data(ActionEnum.SUCCEED_LOGIN.getI(),null,null,String.valueOf(usid)/*Tokengen*/).packToJson().toString().getBytes());
                }else{
                    out.write(new Data(ActionEnum.ERROR_LOGIN.getI(),null,null,null ).packToJson().toString().getBytes());
                }
            }else if(receive.getAction() == ActionEnum.GET_LAST_CHATS.getI()){
                //Debug
                Chat a = new Chat(1,"s",2);
                Chat b = new Chat(3,"Hallo",4);
                Chat[] t = {a,b};

                out.write(new Data(ActionEnum.SUCCEED_GET_LAST_CHATS.getI(),t).packToJson().toString().getBytes());
            }

            Thread.sleep(10);
        }
    }


    public static void main(String[] args) {
        new Main();
    }
}
//Start ma dein Client !!!!
