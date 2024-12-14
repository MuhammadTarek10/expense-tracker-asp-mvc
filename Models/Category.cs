using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace expense_tracker.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string? Title { get; set; }

        [Column(TypeName = "nvarchar(250)")]
        public string? Description { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string Icon { get; set; } = string.Empty;

        [Column(TypeName = "nvarchar(50)")]
        public string Type { get; set; } = "Expense";
    }
}
