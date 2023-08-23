using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManagement.Models
{
    public class Task
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TaskId { get; set; }
        public string TaskTitle { get; set; }
        public string Description { get; set; }
        public DateTime SubmissionDate { get; set; }
        public bool IsCompleted { get; set; }

        [ForeignKey("UserId")]
        public int UserId { get; set; }
    }

}
