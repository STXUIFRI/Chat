package apiSharp;


import org.json.JSONObject;

import java.io.DataInputStream;
import java.io.DataOutputStream;
import java.io.IOException;
import java.net.Socket;
import java.util.Arrays;
import java.util.Scanner;

public class Main {

    private JSONObject toSendJson;
    private JSONObject resiveJson;

    private Data data = new Data();

    public Main() {
        toSendJson = data.packToJson();

        startGame();
        startClient(toSendJson);
    }

    public static void main(String[] args) {
        new Main();

    }

    public void startGame() {
        new Thread(() -> {
            Scanner sc = new Scanner(System.in);
            System.out.println("Game started...");
            while (true) {
                sc.next();
                System.out.println("Game Triggered");
                data.setHealth(data.getHealth() - 10);

            }
        }).start();


    }

    public void startClient(JSONObject toSend) {
        try {
            Socket client = new Socket("25.84.113.137", 6969);
            client.setSoTimeout(10000);

            System.out.println("Trying to Connect to the Server...");

            DataOutputStream out = new DataOutputStream(client.getOutputStream());
            DataInputStream in = new DataInputStream(client.getInputStream());

            JSONObject JsonSend = toSend;
            System.out.println("Client Started...");
            while (true) {
                JsonSend = data.packToJson();
                send(JsonSend, out);
                resive(in);

                System.out.println(Arrays.toString(data.list));

            }
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    private void resive(DataInputStream in) throws IOException {
        StringBuilder s = new StringBuilder();
        while (in.available() == 0) ;
        while (in.available() > 0) {
            s.append((char) in.readByte());
        }
        System.out.println(s.toString());

        resiveJson = new JSONObject(s.toString());

        data.readFromJson(resiveJson);
    }

    private void send(JSONObject toSend, DataOutputStream out) throws IOException {
        out.write(toSend.toString().getBytes());
        System.out.println("Massage send.");
    }
}