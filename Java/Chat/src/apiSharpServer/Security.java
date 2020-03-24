package apiSharpServer;

import SqlConnection.Connection;
import apiSharpServer.data.Data;

import java.nio.charset.StandardCharsets;
import java.security.MessageDigest;
import java.security.NoSuchAlgorithmException;
import java.util.Random;

public class Security {
    private Data data;
    private Connection con;
    private String token;

    public Security() {

    }

    public boolean check(Data data, Connection con, String token) {
        this.data = data;
        this.con = con;
        this.token = token;
        if (data.getLogin() != null) {
            hashPassword();
            return true;
        } else if (checkToken()) {
            return true;
        }
        return false;

    }

    private boolean checkToken() {
        if (!token.equals(data.getToken())) {
            return false;
        } else {
            return true;
        }
    }

    public void hashPassword() {
        try {

            String pass = data.getLogin().getPassword();
            String salt;

            if (data.getAction() == ActionEnum.REGISTER.getI()) {
                salt = saltGen();
                pass += salt;
            } else {
                String encrypedPass = con.getPasswordByUserID(data.getLogin().getName());
                if (encrypedPass != null) {
                    salt = encrypedPass.substring(encrypedPass.length() - 8);
                    pass += salt;
                } else {
                    return;
                }
            }
            System.out.println(salt);
            MessageDigest digest = null;
            digest = MessageDigest.getInstance("SHA-256");
            byte[] encodedhash = digest.digest(pass.getBytes(StandardCharsets.UTF_8));

            data.getLogin().setPassword(bytesToHex(encodedhash) + salt);

        } catch (NoSuchAlgorithmException e) {
            e.printStackTrace();
        }
    }

    private String saltGen() {
        Random r = new Random();
        StringBuilder salt = new StringBuilder();
        for (int i = 0; i < 8; i++) {
            salt.append((char) (r.nextInt(87) + 36));
        }
        return salt.toString();
    }

    private static String bytesToHex(byte[] hash) {
        StringBuffer hexString = new StringBuffer();
        for (int i = 0; i < hash.length; i++) {
            String hex = Integer.toHexString(0xff & hash[i]);
            if (hex.length() == 1) hexString.append('0');
            hexString.append(hex);
        }
        return hexString.toString();
    }
}
