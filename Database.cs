using System.Data;
using System.Data.SqlClient;

public class Database
{
    string connectionString = "";
    static Database instance = null;

    public void Execute(string sql)
    {
        if (string.IsNullOrEmpty(connectionString)) {
            throw new Exception("Not connected to database");
        }

        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            conn.Open();
            SqlCommand command = conn.CreateCommand();
            command.CommandText = sql;
            command.ExecuteNonQuery();
        }
    }
    public SqlDataReader Query(string sql)
    {
        if (string.IsNullOrEmpty(connectionString)) {
            throw new Exception("Not connected to database");
        }

        SqlConnection conn = new SqlConnection(connectionString);
        conn.Open();
        SqlCommand command = conn.CreateCommand();
        command.CommandText = sql;
        return command.ExecuteReader();
    }

    public void CreateUser(string username, string pin, decimal balance) 
    {
        Execute($"INSERT INTO users (Username, Pin, Balance) VALUES ('{username}', '{pin}', {balance})");
    }

    public List<User> GetUsers()
    {
        List<User> users = new();
        SqlDataReader reader = Query("SELECT username, pin, balance FROM users");
        while (reader.NextResult()) 
        {
            User user = new User(
                reader.GetString(0), //The username
                reader.GetString(1), //The pin
                reader.GetDecimal(2) //The balance
            );
            users.Add(user);
        }
        return users;
    }

    public bool Authenticate(string username, string pin)
    {
        SqlDataReader reader = Query("SELECT username FROM users WHERE username='{username}' AND pin='{pin}'");
        if (reader.NextResult()) {
            return true;
        }
        return false;
    }

    public static Database Connect(string username, string password) 
    {
        // Create connectionstring that connects to an SQL Server database 
        //  using username and password
        SqlConnectionStringBuilder builder = new();

        // Set the server
        builder.DataSource = "localhost";
        
        // Set the database
        builder.InitialCatalog = "scambank";

        // Add the username
        builder.UserID = username;

        // Add the password
        builder.Password = password;

        //Alternative to the above
        //connectionString = $"Data Source=localhost;Initial Catalog=scambank;User Id={username};Password={password}"

        instance = new Database() 
        {
            connectionString = builder.ToString()
        };
        return instance;
    }
    public static Database Connect()
    {
        return instance;
    }

    public void RemoveTables()
    {
        Execute("DROP TABLE users;");
    }
    public void CreateTables() 
    {
        Execute(@"CREATE TABLE users (
            username VARCHAR(16) PRIMARY KEY,
            pin VARCHAR(32) NOT NULL,
            balance DECIMAL
        )");
    }
}