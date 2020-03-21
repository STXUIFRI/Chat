package apiSharpServer;

import java.io.DataInputStream;
import java.io.DataOutputStream;
import java.io.IOException;
import java.net.ServerSocket;
import java.net.Socket;
import java.util.LinkedList;
import java.util.List;
import java.util.concurrent.ExecutionException;

public class Main {

    private List<Thread> clients = new LinkedList<>();

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
                    System.out.print("Waiting for Client...");
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

                StringBuilder stringIn;
                while (true) {
                    while (in.available() == 0) {
                        if(client.isClosed())
                            return;
                        Thread.sleep(10);
                    }
                    stringIn = new StringBuilder();

                    while (in.available() > 0) {
                        stringIn.append((char) in.readByte());
                    }
                    System.out.println(stringIn.toString());


                    Thread.sleep(10);
                }
            }catch (Exception e){
                e.printStackTrace();
            }
        });
        clientThread.start();

        clients.add(clientThread);
    }


    public static void main(String[] args) {
        new Main();
    }
}
//Start ma dein Client !!!!
