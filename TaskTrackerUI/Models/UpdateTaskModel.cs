using System.ComponentModel.DataAnnotations;

namespace TaskTrackerUI.Models
{
    public class UpdateTaskModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Görev açıklaması zorunludur."), MaxLength(1000)]
        public string? Description { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        [Required]
        public int PriorityLevel { get; set; }

        [Required]
        public int TaskStateLevel { get; set; }

        [Required]
        public Guid UserId { get; set; }
    }
}
