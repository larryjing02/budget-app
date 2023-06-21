using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniBudget {

    [Table("Expenses")]
    public class ExpenseModel {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int ExpenseId { get; set; }
        [Required]
        public decimal Amount { get; set; }
        [Required]
        public string? Category { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public string? Description { get; set; }
    }
}