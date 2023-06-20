namespace MiniBudget {
    public class Program {

        private static readonly string[] CATEGORIES = {"Housing", "Transportation", "Groceries", "Dining", "Utilities", "Health and Wellness", "Clothing", "Education", "Entertainment", "Miscellaneous"};
        private static List<ExpenseItem> _expenseItems = new List<ExpenseItem>();

        public static void Main(string[] args) {
            Console.WriteLine("===========================================");
            Console.WriteLine("============== Budgeting App ==============");
            Console.WriteLine("===========================================");
            bool repeat = true;
            while (repeat) {
                ListOptions(); 
                Console.Write("Your choice: ");
                var opt = Console.ReadLine();
                switch (opt) {
                    case "1":
                        AddExpense();
                        break;
                    case "2":
                        ViewExpenses();
                        break;
                    case "3":
                        EditExpense();
                        break;
                    case "4":
                        DeleteExpense();
                        break;
                    case "5":
                        repeat = false;
                        break;
                    default:
                        Console.WriteLine("Invalid option! Please try again.");
                        break;
                }
            }
        }

        private static void ListOptions() {
            Console.WriteLine("Please choose one of the following options: ");
            Console.WriteLine("    1. Add an expense");
            Console.WriteLine("    2. View expenses");
            Console.WriteLine("    3. Edit an expense");
            Console.WriteLine("    4. Delete an expense");
            Console.WriteLine("    5. Exit");
        }

        private static void AddExpense() {
            Console.WriteLine("Adding a new expense!");
            // Populate fields from user input
            decimal amount = GetUserAmount();
            string category = GetUserCategory();
            DateTime date = GetUserDate();
            string? description = GetUserDescription();
            Program._expenseItems.Add(new ExpenseItem(amount, category, date, description));
            Console.WriteLine("Expense successfully added!");
            Console.WriteLine("Press any key to return to the main menu.");
            Console.ReadKey(true);
        }

        private static decimal GetUserAmount() {
            decimal amount;
            Console.Write("Please enter the expense amount: ");
            while (!Decimal.TryParse(Console.ReadLine(), out amount)) {
                Console.Write("Invalid format. Please enter expense amount as a decimal number: ");
            }
            return amount;
        }

        private static string GetUserCategory() {
            Console.WriteLine("Which category is your expense? Options:");
            for (int i = 0; i < Program.CATEGORIES.Length; i++) {
                Console.WriteLine($"    {i+1}. {Program.CATEGORIES[i]}");
            }
            Console.Write("Please enter the number that best fits your expense: ");
            int ind;
            while (!int.TryParse(Console.ReadLine(), out ind) | ind <= 0 | ind > Program.CATEGORIES.Length) {
                Console.Write($"Invalid format. Please enter number (1 - {Program.CATEGORIES.Length}): ");
            }
            return Program.CATEGORIES[ind - 1];
        }

        private static DateTime GetUserDate() {
            DateTime date;
            Console.Write("Please enter the expense date/time (mm/dd/yy hh:mm tt): ");
            while (!DateTime.TryParse(Console.ReadLine(), out date)) {
                Console.Write("Invalid format. Please enter expense date/time (mm/dd/yy hh:mm tt): ");
            }
            return date;
        }

        private static string? GetUserDescription() {
            Console.Write("Please enter a short description of the expense (press enter to skip): ");
            var input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input)) return null;
            return input;
        }

        private static void ViewExpenses() {
            Console.WriteLine("Viewing expenses!");
            if (Program._expenseItems.Count == 0) {
                Console.WriteLine("No expenses to view!");
                return;
            }
            // Print out expenses in tabular format
            Console.WriteLine(" {0, -2} | {1, -9} | {2, -20} | {3, -20} | {4, -30}", "ID", "Amount", "Category", "Date", "Description");
            Console.WriteLine(new string('-', 80));
            int count = 1;
            foreach (ExpenseItem item in Program._expenseItems) {
                Console.WriteLine(" {0, -2} | {1, -9} | {2, -20} | {3, -20} | {4, -30}", 
                    count++,
                    "$" + item.Amount,
                    item.Category,
                    item.Date.ToString("MM/dd/yyyy hh:mm tt"),
                    item.Description ?? "N/A");
            }
            Console.WriteLine("Press any key to return to the main menu.");
            Console.ReadKey(true);
        }

        private static void EditExpense() {
            Console.WriteLine("Editing an expense!");
            if (Program._expenseItems.Count == 0) {
                Console.WriteLine("No expenses to edit!");
                return;
            }
            int ind, field;
            Console.Write("Enter the number of the expense you wish to edit: ");
            while (!int.TryParse(Console.ReadLine(), out ind) | ind <= 0 | ind > Program._expenseItems.Count) {
                Console.Write($"Invalid format. Please enter number (1 - {Program._expenseItems.Count}): ");
            }
            var expense = Program._expenseItems[ind - 1];
            Console.WriteLine("You selected the following expense: ");
            Console.WriteLine($"1. Amount (${expense.Amount})");
            Console.WriteLine($"2. Category ({expense.Category})");
            Console.WriteLine($"3. Date ({expense.Date.ToString("MM/dd/yyyy hh:mm tt")})");
            Console.WriteLine($"4. Description ({expense.Description ?? "N/A"})");
            Console.Write("Which field would you like to edit? ");
            while (!int.TryParse(Console.ReadLine(), out field) | field <= 0 | field > 4) {
                Console.Write("Invalid format. Please enter field number (1 - 4): ");
            }
            switch (field) {
                case 1:
                    Console.WriteLine("Editing the expense amount!");
                    expense.Amount = GetUserAmount();
                    break;
                case 2:
                    Console.WriteLine("Editing the expense category!");
                    expense.Category = GetUserCategory();
                    break;
                case 3:
                    Console.WriteLine("Editing the expense date!");
                    expense.Date = GetUserDate();
                    break;
                case 4:
                    Console.WriteLine("Editing the expense description!");
                    expense.Description = GetUserDescription();
                    break;
            }
            Console.WriteLine("Expense successfully edited!");
            Console.WriteLine("Press any key to return to the main menu.");
            Console.ReadKey(true);
        }

        private static void DeleteExpense() {
            Console.WriteLine("Deleting an expense!");
        }
    }
}