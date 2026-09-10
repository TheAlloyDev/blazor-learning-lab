using System.ComponentModel.DataAnnotations;

namespace RecipeManagerApp.Data;

public class Recipe
{
    // add Id: Unique identifier
    public int Id { get; set; } = int.MinValue;
    // add Name: Name of the recipe
    [Required(ErrorMessage = "Recipe name is required.")]
    public string Name { get; set; } = string.Empty;
    // add Description: Description of the recipe
    [Required(ErrorMessage = "Recipe description is required.")]
    [StringLength(300, ErrorMessage = "Recipe description cannot exceed 300 characters.")]
    public string Description { get; set; } = string.Empty;
}
