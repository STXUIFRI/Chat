package SqlConnection;

import apiSharpServer.data.Chat;
import apiSharpServer.data.Message;

import java.sql.*;
import java.time.LocalDateTime;
import java.time.format.DateTimeFormatter;
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
            if (testregister(user)) {
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
     * @param user Username
     * @return false if the Username is already in the DB
     * @throws SQLException
     */
    private boolean testregister(String user) throws SQLException {
        String sql = String.format("Select * from user where name='%s'", user);
        ResultSet get = stmt.executeQuery(sql);
        //IDK iod Woks
        if (get.next()) {
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
            String sql = String.format("INSERT INTO chats (creator, title) VAlUES (%s,'%s')", creator, title);
            stmt.executeUpdate(sql);

            // LocalDateTime now = LocalDateTime.now();
            sql = String.format("INSERT INTO relation (relation.user,relation.group,flags) VAlUES ('%s',LAST_INSERT_ID(),%s)", creator, flags);
            stmt.executeUpdate(sql);

        } catch (java.sql.SQLException e) {
            e.printStackTrace();
        }
    }

    public Chat[] getAllChats(int user) {
        try {
            String sql = String.format("SELECT  idchats,creator,title,chats.flags from relation, chats where relation.user='%s' AND chats.idchats = relation.group", user);
            ResultSet res = stmt.executeQuery(sql);

            int size = 0;
            if (res != null) {
                res.last();
                size = res.getRow();
            }

            Chat[] out = new Chat[size];

            res.first();
            for (int i = 0; i < size; i++) {
                Chat c = new Chat();
                c.setTitle(res.getString("title"));
                c.setChatID(res.getInt("idchats"));
                c.setFlags(res.getInt("flags"));
                c.setCreatorID(user);
                out[i] = c;
                res.next();
            }
            return out;
        } catch (java.sql.SQLException e) {
            e.printStackTrace();
        }
        return null;
    }

    public boolean moveIntoChat(String userName, int chat, int flag) {
        try {
            int user =  getUserID(userName);

            String sql = String.format("SELECT * from relation where relation.user=%s AND relation.group=%S AND flags=%s",user,chat,flag);
            ResultSet res = stmt.executeQuery(sql);
            if(res.next())
                return false;

            sql = String.format("INSERT INTO relation (relation.user, relation.group,flags) VAlUES (%s,%s,%s)", user, chat, flag);
            stmt.executeUpdate(sql);
            return true;
        } catch (java.sql.SQLException e) {
            e.printStackTrace();
        }
        return false;
    }
    public Message[] getAllMessages(int chat)  {
        try {
            String sql = String.format("SELECT * from messages where chat=%s", chat);
            ResultSet res = stmt.executeQuery(sql); //TODO java.sql.SQLException: Illegal operation on empty result set.

            int size = 0;
            if (res != null) {
                res.last();
                size = res.getRow();
            }
            Message[] message = new Message[size];
            res.first();
            for (int i = 0; i < size; i++) {
                Message newMessage = new Message();
                newMessage.setSender(res.getInt("sender"));
                newMessage.setText(res.getString("text"));
                newMessage.setChat(res.getInt("chat"));
                newMessage.setFlags(res.getInt("flags"));
                newMessage.setDate(res.getString("date"));

                message[i] = newMessage;
                res.next();
            }
            return message;
        }catch (java.sql.SQLException e) {
            e.printStackTrace();
        }
        return null;
    }

    public void addMessage(String text,int chat,int sender,int flags) {
        try{
        String sql = String.format("INSERT INTO messages (text,chat,sender,date,flags) VALUES('%s',%s,%s,'%s',%s)", text, chat, sender, getDate(), flags);
        stmt.executeUpdate(sql);
        }catch (java.sql.SQLException e) {
            e.printStackTrace();
        }
    }

    private String getDate() {
        LocalDateTime myDateObj = LocalDateTime.now();
        DateTimeFormatter myFormatObj = DateTimeFormatter.ofPattern("dd-MM-yyyy HH:mm:ss");
      return myDateObj.format(myFormatObj);
    }

    public int getUserID(String user) throws SQLException {
        String sql = String.format("Select iduser from user where name = '%s'", user); //TODO java.sql.SQLException: Illegal operation on empty result set.
        ResultSet rs = stmt.executeQuery(sql);
        rs.next();
        return rs.getInt("iduser");
    }

    public String getPasswordByUserID(String user){
        try {
            String sql = String.format("SELECT password from user where name='%s'", user);
            ResultSet res = stmt.executeQuery(sql);

            res.next();
            return res.getString("password");
        }catch (java.sql.SQLException e) {
        e.printStackTrace();
    }
        return null;
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
