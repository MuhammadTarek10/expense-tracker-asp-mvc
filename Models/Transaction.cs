using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace expense_tracker.Models
{
    public class Transaction
    {
        [Key]
        public Guid Id { get; set; }

        public Guid CategoryId { get; set; }
        public required Category category { get; set; }

        public int Amount { get; set; }

        [Column(TypeName = "nvarchar(75)")]
        public string? Note { get; set; }

        public DateTime date { get; set; } = DateTime.Now;
    }
}
