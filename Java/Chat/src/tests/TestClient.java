package tests;

import apiSharpServer.ActionEnum;
import apiSharpServer.data.Message;

import java.io.DataInputStream;
import java.io.DataOutputStream;
import java.io.IOException;
import java.net.Socket;

public class TestClient {

    private Socket client;
    private DataInputStream in;
    private DataOutputStream out;

    public TestClient() {
        try {
            client = new Socket("localhost", 6969);
            client.setSoTimeout(10000);
            System.out.println("Connected");

            in = new DataInputStream(client.getInputStream());
            out = new DataOutputStream(client.getOutputStream());


            registerRequest();
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    private void sendLoginRequest() throws IOException {
        DataTest data = new DataTest(ActionEnum.LOGIN.getI(),"Xuri");
        out.write(data.packToJson().toString().getBytes());
        System.out.printf("Send: %s%n",data.packToJson().toString());
    }

    private void sendMessagesendRequest() throws IOException {
        Message[] m = {new Message("Hi' delete  from messages; ' du.",null,null,1,0)};
        DataTest data = new DataTest(ActionEnum.SEND_MESSAGE.getI(),m,"Xuri");
        out.write(data.packToJson().toString().getBytes());
        System.out.printf("Send: %s%n",data.packToJson().toString());
    }

    private void registerRequest() throws IOException {
        LoginTest l = new LoginTest("Stiffty","HalloWelt",1,12);

        out.write(new DataTest(ActionEnum.REGISTER.getI(),l,"").packToJson().toString().getBytes());
    }
    private void startListenerThread() {
        new Thread(() -> {
            while (true) {
                try {
                    while (in.available() == 0) ;

                    StringBuilder inString = new StringBuilder();
                    while (in.available() > 0) {
                        inString.append((char) in.readByte());
                    }
                    System.out.printf("Response: %s%n",inString.toString());
                } catch (IOException e) {
                    e.printStackTrace();
                }
            }
        }).start();
    }

    public static void main(String[] args) throws InterruptedException {
//        for (int i = 0; i < 100; i++) {
//            new TestClient();
//            Thread.sleep(10);
//        }
        new TestClient();
    }
}
