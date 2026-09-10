# BlazorLearning

A collection of small Blazor WebAssembly apps used for hands-on learning. Each
project lives in its own folder and can be built/run independently, or all
together via `BlazorLearning.slnx`.

## Projects

- **RecipeManagerApp** — a recipe manager used to learn Blazor fundamentals
  (components, routing, forms). See its own `TODO.md` and learning notes.
- **GLFeedbackApp** — a newer Blazor WebAssembly app for further learning
  exercises.

## Getting started

```powershell
dotnet build BlazorLearning.slnx

# Run a specific project
dotnet run --project RecipeManagerApp\RecipeManagerApp.csproj
dotnet run --project GLFeedbackApp\GLFeedbackApp.csproj
```

A single root-level `.gitignore` covers build output (`bin/`, `obj/`) for all
projects in this repo.

## License

This project is licensed under the [MIT License](LICENSE).
