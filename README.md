# Blazor Learning Playground

A collection of small Blazor WebAssembly apps built as hands-on learning
projects. This repository is structured as a shared practice space for working
through core Blazor concepts in focused, repeatable exercises.

Each project lives in its own folder and can be built and run independently, or
all together via `BlazorLearning.slnx`.

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

- **RecipeManagerApp** — a recipe manager used to practice foundational Blazor
  concepts such as components, routing, form handling, and basic state flow.
  See its own `TODO.md` plus the shared notes in `Notes/`.
- **GLFeedbackApp** — a feedback-focused app used to continue practicing forms,
  validation, component composition, and simple in-memory state management.

## Learning focus

This repo is primarily about learning by building. The goal is not to present a
single production application, but to document steady progress across small,
targeted Blazor exercises.

Areas covered so far include:

- component-based UI composition
- routing between pages
- form binding and validation
- basic state management patterns
- iterative note-taking alongside implementation

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

## Notes

The `Notes/` folder contains learning notes, workflow references, and
exercise-specific writeups captured while building these apps. It is included
on purpose as part of the learning record for this repository.

## License

This project is licensed under the [MIT License](LICENSE).
