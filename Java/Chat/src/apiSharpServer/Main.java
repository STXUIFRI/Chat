package apiSharpServer;

import SqlConnection.Connection;

import java.io.IOException;
import java.net.ServerSocket;
import java.net.Socket;
import java.util.*;

public class Main {

    private Map<String, Client> clients = new HashMap<>();
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
                startThread();
                Socket client = server.accept();
                //wait.stop();
                System.out.printf("%nClient Connected: %s%n", client.getLocalAddress());

                newClient(client);
            }

        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    private void newClient(Socket client) {
        String id = genID();
        Client c = new Client(client,connectionDB,security);
        c.setTokenName(id);
        clients.put(id,c);
    }

    private String genID(){
        Random r = new Random();
        StringBuilder salt = new StringBuilder();
        for (int i = 0; i < 8; i++) {
            salt.append((char)(r.nextInt(87)+36));
        }
        return salt.toString();
    }

    private void startThread(){
        new Thread(()->{
            while (true) {
                for (Map.Entry<String, Client> client : clients.entrySet()) {
                    try {
                        client.getValue().listener();
                        //System.out.printf("Listener called: %s.%n",client.getValue().getTokenName());
                    } catch (Exception e) {
                        e.printStackTrace();
                    }
                }
            }
        }).start();
    }

    public static void main(String[] args) {
        new Main();
    }
}
//Start ma dein Client !!!!
