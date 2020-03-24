package apiSharpServer;

import SqlConnection.Connection;

import java.io.IOException;
import java.net.ServerSocket;
import java.net.Socket;
import java.util.*;

public class Main {

    private Map<String, Client> clients = new HashMap<>();
    private List<Client>clientsBuffer = new ArrayList<>();
    private Connection connectionDB = new Connection();
    private Security security = new Security();


    public Main() {
        startServer();
    }

    private void startServer() {
        try {
            ServerSocket server = new ServerSocket(6969);
            startThread();
            while (true) {
                System.out.println("Waiting for Client...");
                // wait.start();
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
        Client c = new Client(client, connectionDB, security);
        c.setTokenName(id);
        clientsBuffer.add(c);
    }

    private String genID() {
        Random r = new Random();
        StringBuilder salt = new StringBuilder();
        for (int i = 0; i < 50; i++) {
            salt.append((char) (r.nextInt(87) + 36));
        }
        return salt.toString();
    }

    private void startThread() {
        new Thread(() -> {
            while (true) {
                for (int i = 0; i < clientsBuffer.size(); i++) {
                    Client c = clientsBuffer.get(i);
                    clients.put(c.getTokenName(),c);

                }
                clientsBuffer = new ArrayList<>();
                for (Map.Entry<String, Client> client : clients.entrySet()) {
                    try {
                        client.getValue().listener();
                        //System.out.println(client.getValue().getConnect().getInputStream().read()+ "   " +clients.size() );
                    } catch (Exception e) {
                        e.printStackTrace();
                    }
                }
                try {
                    Thread.sleep(1);
                } catch (InterruptedException e) {
                    e.printStackTrace();
                }
            }
        }).start();
    }

    public static void main(String[] args) {
        new Main();
    }
}
//Start ma dein Client !!!!
