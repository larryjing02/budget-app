namespace MiniBudget {
    public class Program {
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