# Recipe Manager Learning Notes

This file explains the mechanics behind the Blazor code you built, so you can understand why it works instead of only copying it.

## 1. What each file does

### `Data/Recipe.cs`

This is a model class.

```csharp
public class Recipe
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}
```

Why it exists:

- `Recipe` is the shape of your data.
- Every recipe object must have an `Id`, `Name`, and `Description`.
- Blazor pages can display or modify objects of this type.

Think of it as a template for one recipe.

## 2. How the Home page works

### Route

In `Home.razor`:

```razor
@page "/"
@page "/home"
```

What this means:

- `@page` turns a Razor component into a page.
- `"/"` means this component can open at the site root.
- `"/home"` means this same component can also open at `/home`.

So one component can answer more than one URL.

### Displaying a list

```razor
@foreach (var recipe in Recipes)
{
    <li>
        <strong>@recipe.Name</strong> - @recipe.Description
    </li>
}
```

What is happening:

- `@foreach` is Razor syntax for looping over data.
- `Recipes` is a `List<Recipe>`.
- For each recipe in the list, Blazor creates one `<li>` in the HTML.
- `@recipe.Name` and `@recipe.Description` insert C# values into the UI.

This is called binding data to the UI.

### The links on Home

```razor
<NavLink href="@($"/recipe/{recipe.Id}")" Match="NavLinkMatch.Prefix">View Details</NavLink>
<NavLink href="/addrecipe">Add Recipe</NavLink>
```

What this means:

- `NavLink` is Blazor navigation for moving between pages.
- `href` is the route you want to go to.
- `@($"/recipe/{recipe.Id}")` builds a dynamic URL such as `/recipe/1`.
- That lets each recipe link to its own details page.
- The Add Recipe link goes to a fixed page: `/addrecipe`.

## 3. How the details page receives the recipe id

In `RecipeDetails.razor`:

```razor
@page "/recipe/{Id:int}"
```

What this means:

- This page expects a value in the URL.
- If the URL is `/recipe/2`, then `Id` becomes `2`.
- `:int` means the route value must be an integer.

Then the page declares:

```csharp
[Parameter]
public int Id { get; set; }
```

Why this matters:

- `[Parameter]` tells Blazor that this value comes from outside the component.
- Here, the value comes from the route.

### Loading the matching recipe

```csharp
protected override void OnParametersSet()
{
    Recipe = RecipeStore.Recipes.FirstOrDefault(recipe => recipe.Id == Id);
}
```

What is happening:

- `OnParametersSet()` is a Blazor lifecycle method.
- It runs when route parameters or component parameters are set.
- The code searches the recipe list for the item whose `Id` matches the URL.
- `FirstOrDefault(...)` returns the first match, or `null` if none exists.

That is why this UI logic works:

```razor
@if (Recipe is null)
{
    <p>Recipe not found.</p>
}
else
{
    <h1>@Recipe.Name</h1>
    <p>@Recipe.Description</p>
}
```

The page can safely handle both cases:

- matching recipe found
- no recipe found for that URL

## 4. How the Add Recipe page works

### Two-way binding

```razor
<input @bind="recipeName" placeholder="Recipe Name" />
<textarea @bind="recipeDescription" placeholder="Recipe Description"></textarea>
```

What `@bind` does:

- It connects the HTML input to a C# variable.
- When the user types, the variable updates automatically.
- When the variable changes, the UI can also reflect the new value.

This is called two-way binding.

The backing fields are:

```csharp
private string recipeName = string.Empty;
private string recipeDescription = string.Empty;
```

### Handling the button click

```razor
<button @onclick="SubmitRecipe">Submit</button>
```

What this means:

- `@onclick` connects a browser click event to a C# method.
- When the button is clicked, `SubmitRecipe()` runs.

### Adding a new recipe

```csharp
private void SubmitRecipe()
{
    var nextId = RecipeStore.Recipes.Count == 0 ? 1 : RecipeStore.Recipes.Max(recipe => recipe.Id) + 1;

    RecipeStore.Recipes.Add(new Recipe
    {
        Id = nextId,
        Name = recipeName,
        Description = recipeDescription
    });

    NavManager.NavigateTo("/home");
}
```

Mechanics:

- It calculates the next id.
- It creates a new `Recipe` object.
- It adds that object to the shared recipe list.
- It redirects the user back to the Home page.

### NavigationManager

```razor
@inject NavigationManager NavManager
```

Why this is needed:

- `@inject` gives the component access to a Blazor service.
- `NavigationManager` is the service that can change pages in code.
- `NavManager.NavigateTo("/home")` is programmatic navigation.

Use `NavLink` when the user clicks a visible link.
Use `NavigationManager` when code decides where to go next.

## 5. Why `RecipeStore` was added

In a course example, each page often hardcodes its own list. That is fine for a first demo, but it creates a problem.

Problem:

- Home might show one list.
- Details might search a different list.
- Add Recipe might add to a third list.

That means pages stop sharing the same data.

`RecipeStore.cs` fixes that by keeping one shared list:

```csharp
public static class RecipeStore
{
    public static List<Recipe> Recipes { get; } = ...
}
```

Why this helps:

- Home reads from the shared list.
- Details reads from the same shared list.
- Add Recipe writes to that same list.

So when you add a recipe, Home and Details can both see it.

This is not a database. It is only in-memory storage while the app is running.

## 6. The difference between markup and `@code`

In a Razor component, you usually have two parts.

Markup:

```razor
<h3>Recipe List</h3>
```

This defines what the user sees.

Code:

```razor
@code {
    private List<Recipe> Recipes => RecipeStore.Recipes;
}
```

This defines the logic and data used by the page.

Blazor combines both parts into one component class behind the scenes.

## 7. Why the NavMenu link works

In `NavMenu.razor`:

```razor
<NavLink class="nav-link" href="/addrecipe">
    Add Recipe
</NavLink>
```

This works because:

- `AddRecipe.razor` has `@page "/addrecipe"`
- the `href` matches that route exactly

That is the key rule for routing:

- page route and link target must match

If they do not match, navigation fails or shows the wrong page.

## 8. Mental model to remember

When you build a Blazor page, think in this order:

1. What route opens this page?
2. What data does this page need?
3. How does the page display that data?
4. Does the page receive parameters from the URL?
5. Does the page need user input with `@bind`?
6. Does the page need actions with `@onclick`?
7. After an action, should the page navigate somewhere else?

If you answer those seven questions, most beginner Blazor pages become much easier to build.

## 9. Small exercises to test your understanding

Try these yourself without AI first:

1. Add a new property named `Category` to `Recipe` and show it on Home.
2. Add a third field on the Add Recipe page for `Category`.
3. Make the details page show `Id` as well as name and description.
4. Add a Delete button on Home that removes a recipe from `RecipeStore.Recipes`.
5. Prevent blank recipes from being added by checking `string.IsNullOrWhiteSpace(...)` before saving.

## 10. Most important takeaway

The mechanic you learned here is not just "how to make links."

It is the relationship between:

- routes with `@page`
- navigation with `NavLink` or `NavigationManager`
- state stored in C# objects
- UI rendering with Razor markup
- events and binding with `@onclick` and `@bind`

That combination is the core of a basic Blazor app.