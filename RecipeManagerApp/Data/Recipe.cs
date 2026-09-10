namespace RecipeManagerApp.Data;

public class Recipe
{
    // add Id: Unique identifier
    public int Id { get; set; } = int.MinValue;
    // add Name: Name of the recipe
    public string Name { get; set; } = string.Empty;
    // add Description: Description of the recipe
    public string Description { get; set; } = string.Empty;
}
