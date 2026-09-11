using System.ComponentModel.DataAnnotations;

namespace GLFeedbackApp.Models;

// Represents one feedback entry and defines the validation rules for the form.
public class Feedback
{
    [Required(ErrorMessage = "Please fill out your name.")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please enter your email address.")]
    [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
    public string Email { get; set; } = string.Empty;

    [StringLength(500, ErrorMessage = "Comment cannot exceed 500 characters.")]
    public string Comment { get; set; } = string.Empty;
}