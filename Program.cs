class Program
{

    static void Main(string[] args)
    {
        Bank bank = new Bank();
        Console.WriteLine("---------- WELCOME TO SCAM BANK----------");

        bool exit = false;
        while (!exit)
        {
            Console.WriteLine("Er du kunde eller Manager? (C/M)");
            string userType = Console.ReadLine().ToLower();

            switch (userType)
            {
                case "c":
                    Console.Clear();
                    Console.Write("Enter your username: ");
                    string username = Console.ReadLine().ToLower();
                    Console.Write("Enter your PIN: ");
                    string pin = Console.ReadLine();
                    if (bank.Authenticate(username, pin))
                    {
                        bank.CurrentUser = username;
                        Menu.ShowCustomerMenu(bank, username);
                        Console.Clear();
                    }
                    else
                    {
                        Console.WriteLine("Invalid username or PIN.");
                    }
                    break;
                case "m":
                    Console.Clear();
                    Menu.ManagerMenu(bank);

                    break;
                default:
                    Console.WriteLine("Forkert input tryk C eller M");
                    break;
            }
            Console.Clear();
            Console.WriteLine("Vil du afslutte bankprogrammet? (Y/N)");
            string choice = Console.ReadLine().ToLower();
            if (choice == "y")
            {
                exit = true;
            }
        }
    }
}
