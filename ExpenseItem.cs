namespace MiniBudget {

    // In-memory expense object (deprecated - see ExpenseModel.cs)
    public class ExpenseItem {
        public decimal Amount { get; set; }
        public string Category { get; set; }
        public DateTime Date { get; set; }
        public string? Description { get; set; }

        public ExpenseItem(decimal amount, string category, DateTime date, string? description = null) {
            Amount = amount;
            Category = category;
            Date = date;
            Description = description;
        }
    }
}