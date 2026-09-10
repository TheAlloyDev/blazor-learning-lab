# BlazorLearning

A collection of small Blazor WebAssembly apps used for hands-on learning. Each
project lives in its own folder and can be built/run independently, or all
together via `BlazorLearning.slnx`.

## Repo structure

```text
BlazorLearning/
  GLFeedbackApp/
  Notes/
  RecipeManagerApp/
  BlazorLearning.slnx
  README.md
```

- `RecipeManagerApp/` contains the recipe exercise app.
- `GLFeedbackApp/` contains the feedback exercise app.
- `Notes/` contains shared learning notes and workflow references used across exercises.

## Projects

- **RecipeManagerApp** — a recipe manager used to learn Blazor fundamentals
  (components, routing, forms). See its own `TODO.md` plus the shared notes in `Notes/`.
- **GLFeedbackApp** — a newer Blazor WebAssembly app for further learning
  exercises.

## Shared notes

- `Notes/Recipe-Learning-Notes.md`
- `Notes/Blazor-Forms-Learning-Notes.md`
- `Notes/Next-Exercise-Workflow.md`

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
