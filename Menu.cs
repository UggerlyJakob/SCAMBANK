using System.Net;

public class Menu
{
    public static void ShowCustomerMenu(Bank bank, string currentUser)
    {
        bool exit = false;
        while (!exit)
        {
            
            Console.WriteLine("Welcome to the Bank!");
            Console.WriteLine("1. Check Balance");
            Console.WriteLine("2. Deposit");
            Console.WriteLine("3. Withdraw");
            Console.WriteLine("4. Exit");
            Console.WriteLine("Vælg et punkt på menuen");
            string choice = Console.ReadLine();
            Console.Clear();
            switch (choice)
            {
                case "1":
                    Console.Clear();
                    Console.WriteLine($"Din balance: {bank.GetBalance(currentUser):C}");
                    break;
                case "2":
                    Console.Clear();
                    Console.Write("Indsæt: ");
                    if (decimal.TryParse(Console.ReadLine(), out decimal depositAmount))
                    {
                        bank.Deposit(currentUser, depositAmount);
                    }
                    else
                    {
                        Console.WriteLine("Du har ikke så meget at indsætte, indsæt nu det rigtige");
                    }
                    break;
                case "3":
                    Console.Clear();
                    Console.Write("Hvor meget ønsker du at hæve: ");
                    if (decimal.TryParse(Console.ReadLine(), out decimal withdrawalAmount))
                    {
                        bank.Withdraw(currentUser, withdrawalAmount);
                    }
                    else
                    {
                        Console.WriteLine("Du har slet ikke så meget på kontoen");
                    }
                    break;
                case "4":
                    Console.Clear();
                    Console.WriteLine("Exiting...");
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Tryk på et rigtigt tal ikke noget hokus pokus");
                    break;
            }
        }
    }
    public static void ManagerMenu(Bank bank)
    {
        Console.WriteLine("Manager Menu:");
        Console.WriteLine("1. Add Customer");
        Console.WriteLine("2. Remove Customer");
        Console.WriteLine("3. Show All Customers");
        Console.WriteLine("4. Database");
        Console.Write("Enter your choice: ");
        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                Console.Clear();
                Console.Write("Enter new username: ");
                string newUsername = Console.ReadLine().ToLower();

                Console.Write("Enter PIN: ");
                string newPin = Console.ReadLine();

                Console.Write("Enter initial balance: ");
                decimal initialBalance;
                while (!decimal.TryParse(Console.ReadLine(), out initialBalance))
                {
                    Console.WriteLine("Invalid input. Please enter a valid decimal.");
                }

                bank.AddUser(newUsername, newPin, initialBalance);
                break;
            case "2":
                Console.Clear();
                Console.Write("Enter username to remove: ");
                string usernameToRemove = Console.ReadLine().ToLower();
                bank.RemoveUser(usernameToRemove);
                break;
            case "3":
                Console.Clear();
                bank.ShowAllCustomers();
                break;
            case "4":
                Console.Clear();
                DatabaseMenu();
                break;
            default:
                Console.Clear();
                Console.WriteLine("Invalid choice. Please try again.");
                break;
        }
    }
    public static void DatabaseMenu() 
    {
        Console.WriteLine("Enter username:");
        string username = Console.ReadLine();
        Console.WriteLine("Enter password");
        string password = Console.ReadLine();

        Console.WriteLine("Database Menu:");
        Console.WriteLine("1. Create tables");
        Console.WriteLine("2. Remove tables");
        Console.WriteLine("3. Go back");
        Console.Write("Enter your choice: ");
        int choice = 0;
        Database database = Database.Connect(username, password);
        do 
        {
            int.TryParse(Console.ReadLine(), out choice);
            switch (choice) 
            {
                case 1:
                    database.CreateTables();
                    break;
                case 2:
                    database.RemoveTables();;
                    break;
                case 3:
                    return;
            }
        } while (choice < 1 && choice > 3);
    }
}