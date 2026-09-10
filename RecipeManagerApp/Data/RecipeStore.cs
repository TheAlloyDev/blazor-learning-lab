namespace RecipeManagerApp.Data;

public static class RecipeStore
{
    public static List<Recipe> Recipes { get; } =
    [
        new Recipe
        {
            Id = 1,
            Name = "Spaghetti Bolognese",
            Description = "A classic Italian pasta dish with rich meat sauce."
        },
        new Recipe
        {
            Id = 2,
            Name = "Chicken Curry",
            Description = "A flavorful and spicy chicken curry."
        },
        new Recipe
        {
            Id = 3,
            Name = "Chocolate Cake",
            Description = "A moist and rich chocolate cake."
        }
    ];
}