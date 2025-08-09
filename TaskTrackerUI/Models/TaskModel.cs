using System.ComponentModel.DataAnnotations;

namespace TaskTrackerUI.Models
{
    public class TaskModel()
    {
        [Required(ErrorMessage = "Görev başlığı zorunludur."), MaxLength(100)]
        public string? Title { get; set; }

        [Required(ErrorMessage = "Görev açıklaması zorunludur."), MaxLength(1000)]
        public string? Description { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        [Required]
        public int PriorityLevel { get; set; }

        [Required]
        public int StateLevel { get; set; }

        [Required]
        public Guid UserId { get; set; }
    }
}
