public class User
{
    public string Username { get; }
    public string Pin { get; }

    public decimal Balance { get; set; }

    public User(string username, string pin, decimal initialBalance)
    {
        Username = username;
        Pin = pin;
        Balance = initialBalance;

    }

}
