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
    PreparedStatement pSmtmt;

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
                sendDBRequest("INSERT INTO user (name, password,male,age) VAlUES (?,?,?,?)",4,false,user,password,male,age);
//                String sql = String.format("INSERT INTO user (name, password,male,age) VAlUES ('%s','%s',%s,%s)", user, password, male, age);
//                stmt.executeUpdate(sql);
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
        ResultSet get = sendDBRequest("Select * from user where name=?",1,true,user);
//        String sql = String.format("Select * from user where name='%s'", user);
//        ResultSet get = stmt.executeQuery(sql);
        //IDK iod Woks
        if (get.next()) {
            //wenn es schon einen gibt
            return false;
        }
        return true;
    }

    private ResultSet sendDBRequest(String command, int argumentCounter,boolean query, Object... args) {
        try {
            pSmtmt = con.prepareStatement(command);

            for (int i = 0; i < argumentCounter; i++) {

                pSmtmt.setObject(i+1,args[i]);
            }
            if(query) {
                return pSmtmt.executeQuery();
            }else{
                pSmtmt.executeUpdate();
                return null;
            }

        } catch (SQLException e) {
            e.printStackTrace();
        }
        return null;
    }

    public int loginUser(String name, String password) {
        try {
            pSmtmt = con.prepareStatement("SELECT iduser from user where name=? AND password=?");

            pSmtmt.setString(1, name);
            pSmtmt.setString(2, password);
            ResultSet res = pSmtmt.executeQuery();

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

//            String sql = String.format("INSERT INTO chats (creator, title) VAlUES (%s,'%s')", creator, title);
//            System.out.println(sql);
//            stmt.executeUpdate(sql);
            sendDBRequest("INSERT INTO chats (creator, title) VAlUES (?,?)",2,false,creator,title);
            // LocalDateTime now = LocalDateTime.now();
            sendDBRequest("INSERT INTO relation (relation.user,relation.group,flags) VAlUES (?,LAST_INSERT_ID(),?)",2,false,creator,flags);
//            String sql = String.format("INSERT INTO relation (relation.user,relation.group,flags) VAlUES ('%s',LAST_INSERT_ID(),%s)", creator, flags);
//            stmt.executeUpdate(sql);

    }

    public Chat[] getAllChats(int user) {
        try {
            ResultSet res =  sendDBRequest("SELECT  idchats,creator,title,chats.flags from relation, chats where relation.user=? AND chats.idchats = relation.group",1,true,user);
//            String sql = String.format("SELECT  idchats,creator,title,chats.flags from relation, chats where relation.user='%s' AND chats.idchats = relation.group", user);
//            ResultSet res = stmt.executeQuery(sql);

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
            int user = getUserID(userName);

            ResultSet res = sendDBRequest("SELECT * from relation where relation.user=? AND relation.group=? AND flags=?",3,true,user,chat,flag);
//            String sql = String.format("SELECT * from relation where relation.user=%s AND relation.group=%S AND flags=%s", user, chat, flag);
//            ResultSet res = stmt.executeQuery(sql);
            if (res.next())
                return false;

            sendDBRequest("INSERT INTO relation (relation.user, relation.group,flags) VAlUES (?,?,?)",3,false,user,chat,flag);
//            sql = String.format("INSERT INTO relation (relation.user, relation.group,flags) VAlUES (%s,%s,%s)", user, chat, flag);
//            stmt.executeUpdate(sql);
            return true;
        } catch (java.sql.SQLException e) {
            e.printStackTrace();
        }
        return false;
    }

    public Message[] getAllMessages(int chat) {
        try {
            ResultSet res = sendDBRequest("Select messages.*, user.name FROM messages,user where user.iduser = messages.sender AND messages.chat = ?",1,true,chat);
//            String sql = String.format("Select messages.*, user.name FROM messages,user where user.iduser = messages.sender AND messages.chat = %s", chat);
//            ResultSet res = stmt.executeQuery(sql); //TODO java.sql.SQLException: Illegal operation on empty result set.

            int size = 0;
            if (res != null) {
                res.last();
                size = res.getRow();
            }
            Message[] message = new Message[size];
            res.first();
            for (int i = 0; i < size; i++) {
                Message newMessage = new Message();
                newMessage.setSender(res.getString("name"));
                newMessage.setText(res.getString("text"));
                newMessage.setChat(res.getInt("chat"));
                newMessage.setFlags(res.getInt("flags"));
                newMessage.setDate(res.getString("date"));

                message[i] = newMessage;
                res.next();
            }
            return message;
        } catch (java.sql.SQLException e) {
            e.printStackTrace();
        }
        return null;
    }

    public void addMessage(String text, int chat, int sender, int flags) {
            sendDBRequest("INSERT INTO messages (text,chat,sender,date,flags) VALUES(?,?,?,?,?)",5,false,text,chat,sender,getDate(),flags);
//            String sql = String.format("INSERT INTO messages (text,chat,sender,date,flags) VALUES('%s',%s,%s,'%s',%s)", text, chat, sender, getDate(), flags);
//            stmt.executeUpdate(sql);
    }

    private String getDate() {
        LocalDateTime myDateObj = LocalDateTime.now();
        DateTimeFormatter myFormatObj = DateTimeFormatter.ofPattern("dd-MM-yyyy HH:mm:ss");
        return myDateObj.format(myFormatObj);
    }

    public int getUserID(String user) throws SQLException {
        ResultSet rs = sendDBRequest("Select iduser from user where name = ?",1,true,user);
//        String sql = String.format("Select iduser from user where name = '%s'", user); //TODO java.sql.SQLException: Illegal operation on empty result set.
//        ResultSet rs = stmt.executeQuery(sql);
        if(!rs.next()){
            return -1;
        }

        return rs.getInt("iduser");
    }

    public String getPasswordByUserID(String user) {
        try {
            ResultSet res =sendDBRequest("SELECT password from user where name=?",1,true,user);
//            String sql = String.format("SELECT password from user where name='%s'", user);
//            ResultSet res = stmt.executeQuery(sql);

            if(!res.next()){
                return null;
            }
            return res.getString("password");
        } catch (java.sql.SQLException e) {
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
