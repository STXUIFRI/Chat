package SqlConnection;

import java.sql.*;
import java.time.LocalDateTime;
import java.util.Date;
import java.util.Properties;

public class Main {
    private String userName = "Admin";
    private String password = "1234";
    private String dbms = "mysql";
    private String serverName = "localhost";
    private String databaseName = "chat";
    private int portNumber = 3306;

    Connection con;
    Statement stmt;

    public Main() {
        con = connectToDB();
        try {
            stmt = con.createStatement();
        } catch (java.sql.SQLException e) {
            e.printStackTrace();
        }
    }

    public void registerUser(String user, String password, int male, int age) {
        try {
            String sql = String.format("INSERT INTO user (name, password,male,age) VAlUES ('%s','%s',%s,%s)", user, password, male, age);
            stmt.executeUpdate(sql);
        } catch (java.sql.SQLException e) {
            e.printStackTrace();
        }

    }

    public boolean loginUser(String name, String password) {
        try {
            String sql = String.format("SELECT * from user where name='%s' AND password='%s'", name, password);
            ResultSet res = stmt.executeQuery(sql);

            //IDK if Works
            if (!res.next()) {
                return false;
            } else {
                return true;
            }
        } catch (java.sql.SQLException e) {
            e.printStackTrace();
            return false;
        }
    }

    public void createChat(String creator, String title, int flags) {
        try {
            String sql = String.format("INSERT INTO chats (creator, title) VAlUES ('%s','%s')", creator, title);
            stmt.executeUpdate(sql);

            LocalDateTime now = LocalDateTime.now();
            sql = String.format("INSERT INTO relation (user, group,date,flags) VAlUES ('%s','%s',%s)", creator, title, flags);
            stmt.executeUpdate(sql);

        } catch (java.sql.SQLException e) {
            e.printStackTrace();
        }
    }

    public void moveIntoChat(String user,String chat,int flag){
        try{
        String sql = String.format("INSERT INTO chats (creator, title) VAlUES ('%s','%s',%s)", user, chat,flag);
        stmt.executeUpdate(sql);
        } catch (java.sql.SQLException e) {
            e.printStackTrace();
        }
    }



    private Connection connectToDB() {
        Connection conn = null;
        try {
            Class.forName("com.mysql.jdbc.Driver");

            Properties connectionProps = new Properties();
            connectionProps.put("user", this.userName);
            connectionProps.put("password", this.password);

            if (this.dbms.equals("mysql")) {
                conn = DriverManager.getConnection(
                        "jdbc:" + this.dbms + "://" +
                                this.serverName +
                                ":" + this.portNumber + "/" +
                                databaseName +
                                "?useUnicode=true&useJDBCCompliantTimezoneShift=true&useLegacyDatetimeCode=false&serverTimezone=Europe/Berlin",
                        connectionProps);
            }
            System.out.println("Connected to database");

            return conn;

//            Statement stmt = conn.createStatement();
//            String sql = "INSERT INTO user (Name, messagesid) VAlUES ('Peter', 2) ";
//
//            stmt.executeUpdate(sql);


        } catch (Exception e) {
            e.printStackTrace();
        }
        return conn;
    }

}
