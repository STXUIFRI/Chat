package SqlConnection;

import java.sql.*;
import java.time.LocalDateTime;
import java.util.Properties;

public class Connection {
    private String userName = "Admin";
    private String password = "1234";
    private String dbms = "mysql";
    private String serverName = "localhost";
    private String databaseName = "chat";
    private int portNumber = 3306;

    java.sql.Connection con;
    Statement stmt;

    public Connection() {
        con = connectToDB();
        try {
            stmt = con.createStatement();
        } catch (java.sql.SQLException e) {
            e.printStackTrace();
        }
    }

    public boolean registerUser(String user, String password, int male, int age) {
        try {
            if(testregister(user)){
                String sql = String.format("INSERT INTO user (name, password,male,age) VAlUES ('%s','%s',%s,%s)", user, password, male, age);
                stmt.executeUpdate(sql);
                return true;
            }

          return false;
        } catch (java.sql.SQLException e) {
            e.printStackTrace();
        }

        return false;
    }

    /**
     *
     * @param user Username
     * @return false if the Username is already in the DB
     * @throws SQLException
     */
    private boolean testregister(String user) throws SQLException {
        String sql = String.format("Select * from user where name='%s'", user);
        ResultSet get = stmt.executeQuery(sql);
        //IDK iod Woks
        if(get.next()){
            //wenn es schon einen gibt
            return false;
        }
        return true;
    }

    public int loginUser(String name, String password) {
        try {
            String sql = String.format("SELECT iduser from user where name='%s' AND password='%s'", name, password);
            ResultSet res = stmt.executeQuery(sql);

            //IDK if Works
            if (!res.next()) {
                return -1;
            } else {
                return res.getInt("iduser");
            }
        } catch (java.sql.SQLException e) {
            e.printStackTrace();
            return -1;
        }
    }

    public void createChat(int creator, String title, int flags) {
        try {
            String sql = String.format("INSERT INTO chats (creator, title) OUTPUT Inserted.PrimaryKey VAlUES ('%s','%s')", creator, title);
            ResultSet res = stmt.executeQuery(sql);

            LocalDateTime now = LocalDateTime.now();
            sql = String.format("INSERT INTO relation (user, group,flags) VAlUES ('%s','%s',%s)", creator, res.getInt("chatid"), flags);
            stmt.executeUpdate(sql);

        } catch (java.sql.SQLException e) {
            e.printStackTrace();
        }
    }

    public void moveIntoChat(int user,String chat,int flag){
        try{
        String sql = String.format("INSERT INTO chats (creator, title) VAlUES ('%s','%s',%s)", user, chat,flag);
        stmt.executeUpdate(sql);
        } catch (java.sql.SQLException e) {
            e.printStackTrace();
        }
    }



    public int getUserID(String user) throws SQLException {
        String sql = String.format("Select iduser from user where name = '%s'",user);
        ResultSet rs = stmt.executeQuery(sql);
        return rs.getInt("iduser");
    }

    private java.sql.Connection connectToDB() {
        java.sql.Connection conn = null;
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
