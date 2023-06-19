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
        }

        private static void EditExpense() {
            Console.WriteLine("Editing an expense!");
        }

        private static void DeleteExpense() {
            Console.WriteLine("Deleting an expense!");
        }
    }
}