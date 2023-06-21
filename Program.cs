namespace MiniBudget {
    public class Program {

        private static readonly string[] CATEGORIES = {"Housing", "Transportation", "Groceries", "Dining", "Utilities", "Health and Wellness", "Clothing", "Education", "Entertainment", "Miscellaneous"};

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
                        Console.WriteLine("Thank you for using the expense tracker!");
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
            PerformDatabaseOperation(db => {
                var expense = new ExpenseModel {
                    Amount = amount,
                    Category = category,
                    Date = date,
                    Description = description
                };
                db.Expenses.Add(expense);
            });
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
            while (!int.TryParse(Console.ReadLine(), out ind) || ind <= 0 || ind > Program.CATEGORIES.Length) {
                Console.Write($"Invalid format. Please enter number (1 - {Program.CATEGORIES.Length}): ");
            }
            return Program.CATEGORIES[ind - 1];
        }

        private static DateTime GetUserDate() {
            DateTime date;
            Console.Write("Please enter the expense date/time (mm/dd/yy hh:mm tt), or press enter for current date/time: ");
            var userInput = Console.ReadLine();
            while (!String.IsNullOrWhiteSpace(userInput) & !DateTime.TryParse(userInput, out date)) {
                Console.Write("Invalid format. Please enter expense date/time (mm/dd/yy hh:mm tt), or press enter for current date/time: ");
                userInput = Console.ReadLine();
            }
            if (String.IsNullOrWhiteSpace(userInput)) {
                return DateTime.Now;
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
            PerformDatabaseOperation(db => {
                if (db.Expenses.Count() == 0) {
                    Console.WriteLine("No expenses to view!");
                    return;
                }
                int orderField;
                Console.WriteLine("How do you want to order your results?");
                Console.WriteLine("    1. Amount (Highest)");
                Console.WriteLine("    2. Amount (Lowest)");
                Console.WriteLine("    3. Category");
                Console.WriteLine("    4. Date (Oldest)");
                Console.WriteLine("    5. Date (Newest)");
                Console.WriteLine("    6. ID");
                Console.Write("Which field would you like to order by? ");
                while (!int.TryParse(Console.ReadLine(), out orderField) || orderField <= 0 || orderField > 6) {
                    Console.Write("Invalid format. Please enter field number (1 - 6): ");
                }
                List<ExpenseModel> expenses = db.Expenses.ToList();
                switch (orderField) {
                    case 1:
                        Console.WriteLine("Ordering by amount (highest first)!");
                        expenses = expenses.OrderByDescending(e => e.Amount).ToList();
                        break;
                    case 2:
                        Console.WriteLine("Ordering by amount (lowest first)!");
                        expenses = expenses.OrderBy(e => e.Amount).ToList();
                        break;
                    case 3:
                        Console.WriteLine("Ordering by expense category!");
                        expenses = expenses.OrderBy(e => e.Category).ToList();
                        break;
                    case 4:
                        Console.WriteLine("Ordering by date (oldest first)!");
                        expenses = expenses.OrderBy(e => e.Date).ToList();
                        break;
                    case 5:
                        Console.WriteLine("Ordering by date (newest first)!");
                        expenses = expenses.OrderByDescending(e => e.Date).ToList();
                        break;
                    case 6:
                        Console.WriteLine("Ordering by ID!");
                        expenses = expenses.OrderBy(e => e.ExpenseId).ToList();
                        break;
                }
                // Print out expenses in tabular format
                var colFormatStr = " {0, -2} | {1, -9} | {2, -20} | {3, -20} | {4, -30}";
                Console.WriteLine(colFormatStr, "ID", "Amount", "Category", "Date", "Description");
                Console.WriteLine(new string('-', 80));
                foreach (ExpenseModel item in expenses) {
                    Console.WriteLine(colFormatStr, 
                        item.ExpenseId,
                        "$" + item.Amount.ToString("F2"),
                        item.Category,
                        item.Date.ToString("MM/dd/yyyy hh:mm tt"),
                        item.Description ?? "N/A");
                }
            });
            Console.WriteLine("Press any key to return to the main menu.");
            Console.ReadKey(true);
        }

        private static void EditExpense() {
            Console.WriteLine("Editing an expense!");
            PerformDatabaseOperation(db => {
                var numExpenses = db.Expenses.Count();
                if (numExpenses == 0) {
                    Console.WriteLine("No expenses to edit!");
                    return;
                }
                int expenseId, field;
                ExpenseModel? expense;
                Console.Write("Enter the ID of the expense you wish to edit: ");
                while (!int.TryParse(Console.ReadLine(), out expenseId) || (expense = db.Expenses.Find(expenseId)) == null) {
                    Console.Write($"Invalid format. Please enter corresponding ID number: ");
                }
                Console.WriteLine("You selected the following expense: ");
                Console.WriteLine($"    1. Amount (${expense.Amount.ToString("F2")})");
                Console.WriteLine($"    2. Category ({expense.Category})");
                Console.WriteLine($"    3. Date ({expense.Date.ToString("MM/dd/yyyy hh:mm tt")})");
                Console.WriteLine($"    4. Description ({expense.Description ?? "N/A"})");
                Console.Write("Which field would you like to edit? ");
                while (!int.TryParse(Console.ReadLine(), out field) || field <= 0 || field > 4) {
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
            });
            Console.WriteLine("Expense successfully edited!");
            Console.WriteLine("Press any key to return to the main menu.");
            Console.ReadKey(true);
        }

        private static void DeleteExpense() {
            Console.WriteLine("Deleting an expense!");
            PerformDatabaseOperation(db => {
                var numExpenses = db.Expenses.Count();
                if (numExpenses == 0) {
                    Console.WriteLine("No expenses to delete!");
                    return;
                }
                int expenseId;
                ExpenseModel? expense;
                Console.Write("Enter the ID of the expense you wish to delete: ");
                while (!int.TryParse(Console.ReadLine(), out expenseId) || (expense = db.Expenses.Find(expenseId)) == null) {
                    Console.Write($"Invalid format. Please enter corresponding ID number: ");
                }
                Console.WriteLine("You selected the following expense: ");
                Console.WriteLine($"    Amount: ${expense.Amount.ToString("F2")}");
                Console.WriteLine($"    Category: {expense.Category}");
                Console.WriteLine($"    Date: {expense.Date.ToString("MM/dd/yyyy hh:mm tt")}");
                Console.WriteLine($"    Description: {expense.Description ?? "N/A"}");
                Console.WriteLine("Are you sure you want to delete this expense? Press Y to confirm.");
                if (string.Equals(Console.ReadLine(), "y", StringComparison.OrdinalIgnoreCase)) {
                    db.Expenses.Remove(expense);
                    Console.WriteLine("Expense successfully removed!");
                    Console.WriteLine("Press any key to return to the main menu.");
                    Console.ReadKey(true);
                } else {
                    Console.WriteLine("Expense deletion cancelled!");
                }
            });
        }

        // Simple wrapper for database operations
        public static void PerformDatabaseOperation(Action<ExpenseContext> operation) {
            using var db = new ExpenseContext();
            operation(db);
            db.SaveChanges();
        }

    }
}