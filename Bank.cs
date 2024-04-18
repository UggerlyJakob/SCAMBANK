public class Bank
{
    private Dictionary<string, User> users;
    public string CurrentUser { get; set; }
    public Bank()
    {
        users = new Dictionary<string, User>();

        users.Add("Emil", new User("Emil", "12345", 1000));
        users.Add("Jakob", new User("Jakob", "23456", 10000));
    }
    // Tjekker om user og pin passer
    public bool Authenticate(string currentuser, string pin)
    {
        if (users.ContainsKey(currentuser))
        {
            return users[currentuser].Pin == pin;
        }
        return false;
    }
    // Vis Balance
    public decimal GetBalance(string currentuser)
    {
        if (users.ContainsKey(currentuser))
        {
            return users[currentuser].Balance;
        }
        throw new ArgumentException("Kunde ikke fundet");
    }

    // Indsæt penge
    public void Deposit(string username, decimal amount)
    {
        if (users.ContainsKey(username))
        {
            users[username].Balance += amount;
            Console.WriteLine($"Indsat {amount:C} succesfuldt.");

        }
        else
        {
            throw new ArgumentException("Kunde ikke fundet");
        }
    }

    // Hæve penge
    public void Withdraw(string username, decimal amount)
    {
        if (users.ContainsKey(username))
        {
            if (users[username].Balance >= amount)
            {
                users[username].Balance -= amount;
                Console.WriteLine($"Du har hævet {amount:C} succesfuldt");
            }
            else
            {
                Console.WriteLine("Du har ikke penge nok på kontoen!! SLACKER");
            }
        }
        else
        {
            throw new ArgumentException("Kunde ikke fundet");
        }
    }

    public void AddUser(string username, string pin, decimal inititalBalance)
    {
        if (!users.ContainsKey(username))
        {
            users.Add(username, new User(username, pin, inititalBalance));
            Console.WriteLine($"Kunde {username} tilføjet succesfuldt");
        }
        else
        {
            Console.WriteLine($"FEJL - Kunde {username} findes i systemet!");
        }
    }
    public void RemoveUser(string username)
    {
        if (users.ContainsKey(username))
        {
            users.Remove(username);
            Console.WriteLine($"Kunde{username} fjernet succesfuldt");
        }
        else
        {
            Console.WriteLine($"Kunde {username} ikke fundet");
        }
    }

    public void ShowAllCustomers()
    {
        Console.WriteLine("Alle Kunder");
        foreach (var user in users.Values)
        {
            Console.WriteLine($"Kundenavn: {user.Username}, Balance: {user.Balance:C}");
        }

    }

}
