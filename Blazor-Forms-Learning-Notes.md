# Blazor Forms Learning Notes

This file is a reusable guide for building forms in Blazor.

The goal is to understand the mechanics behind forms, binding, submission, and validation so you can build them yourself later.

## 1. Mental model for Blazor forms

When you build a form in Blazor, there are usually four parts working together:

1. A model class that stores the form data.
2. An `EditForm` component that wraps the form.
3. Input components such as `InputText` or `InputSelect` that bind to the model.
4. A submit handler that runs when the form is submitted.

If validation is needed, you add two more pieces:

1. Data annotation attributes on the model.
2. Validation components inside the form.

## 2. The role of `EditForm`

`EditForm` is the main Blazor form component.

Example:

```razor
<EditForm Model="@userDetails" OnValidSubmit="HandleSubmit">
</EditForm>
```

What it does:

- wraps the form UI
- connects the form to a C# object through `Model`
- handles form submission events

What the attributes mean:

- `Model="@userDetails"` tells Blazor which object stores the form values
- `OnValidSubmit="HandleSubmit"` says: only run `HandleSubmit` if validation passes

Think of the model as the container that holds everything the user typed or selected.

## 3. The model class

A form usually needs a model class.

Example:

```csharp
public class UserDetails
{
    public string UserName { get; set; }
    public string EmailAddress { get; set; }
    public string ContactMethod { get; set; }
}
```

Why this exists:

- each property represents one form field
- Blazor binds input components to these properties
- the form submission handler reads values from this object

The model is the single source of truth for the form data.

## 4. `InputText`

Use `InputText` when you want the user to type a short line of text.

Example:

```razor
<div>
    <label>User Name</label>
    <InputText @bind-Value="userDetails.UserName" />
</div>
```

What is happening:

- the label tells the user what the field is for
- `InputText` renders a text box
- `@bind-Value` connects the input to `userDetails.UserName`

Mechanic:

- when the user types, `userDetails.UserName` updates automatically
- when the property changes, the UI can reflect the new value

That is two-way binding.

## 5. `InputSelect`

Use `InputSelect` when the user must choose from a dropdown list.

Example:

```razor
<div>
    <label>Contact Method</label>
    <InputSelect @bind-Value="userDetails.ContactMethod">
        <option value="">Select one</option>
        <option value="Email">Email</option>
        <option value="Phone">Phone</option>
    </InputSelect>
</div>
```

What is happening:

- the dropdown offers a fixed list of choices
- the selected option is stored in `userDetails.ContactMethod`

Use this when free typing would be a bad choice and you want a controlled set of values.

## 6. Buttons and submit flow

Example:

```razor
<button type="submit">Submit</button>
```

Inside an `EditForm`, a submit button tells the form to submit.

Then Blazor checks the `EditForm` events:

- `OnSubmit`
- `OnValidSubmit`
- `OnInvalidSubmit`

Most beginner cases use `OnValidSubmit`.

Why:

- it only runs if the form passes validation
- it keeps validation and processing in a clean order

## 7. Handling form submission

Example:

```csharp
private UserDetails userDetails = new();

private void HandleSubmit()
{
    Console.WriteLine(userDetails.UserName);
    Console.WriteLine(userDetails.ContactMethod);
}
```

What is happening:

- `userDetails` is the model object connected to the form
- when the user submits, `HandleSubmit()` can read the values from that object
- you can save the data, send it to an API, or navigate somewhere else

Important idea:

- the form fields do not need to be read one by one from HTML
- Blazor already placed the values into the model

## 8. Validation with data annotations

Blazor often uses data annotations for validation.

These are attributes placed on the model properties.

Example:

```csharp
using System.ComponentModel.DataAnnotations;

public class UserDetails
{
    [Required(ErrorMessage = "User Name is required")]
    public string UserName { get; set; }

    [EmailAddress(ErrorMessage = "Invalid Email Address")]
    public string EmailAddress { get; set; }
}
```

What this means:

- `[Required]` means the field must have a value
- `[EmailAddress]` means the value must match email format
- `ErrorMessage` lets you control the text shown to the user

Why this is useful:

- validation rules stay close to the data model
- the rules can be reused anywhere the model is used

## 9. `DataAnnotationsValidator`

Inside the form:

```razor
<DataAnnotationsValidator />
```

What it does:

- scans the model for validation attributes
- activates those validation rules during form submission

Without this component, your data annotation attributes will not be applied by the form.

## 10. `ValidationSummary`

Inside the form:

```razor
<ValidationSummary />
```

What it does:

- shows a list of validation errors
- usually appears near the top of the form

Why it helps:

- the user can quickly see what is wrong
- it is a fast way to surface all errors without custom UI first

## 11. Full example

This is the basic shape of a validated Blazor form.

### Model

```csharp
using System.ComponentModel.DataAnnotations;

public class UserDetails
{
    [Required(ErrorMessage = "User Name is required")]
    public string UserName { get; set; } = string.Empty;

    [EmailAddress(ErrorMessage = "Invalid Email Address")]
    public string EmailAddress { get; set; } = string.Empty;

    public string ContactMethod { get; set; } = string.Empty;
}
```

### Razor component

```razor
@page "/contact"

<EditForm Model="@userDetails" OnValidSubmit="HandleSubmit">
    <DataAnnotationsValidator />
    <ValidationSummary />

    <div>
        <label>User Name</label>
        <InputText @bind-Value="userDetails.UserName" />
    </div>

    <div>
        <label>Email Address</label>
        <InputText @bind-Value="userDetails.EmailAddress" />
    </div>

    <div>
        <label>Contact Method</label>
        <InputSelect @bind-Value="userDetails.ContactMethod">
            <option value="">Select one</option>
            <option value="Email">Email</option>
            <option value="Phone">Phone</option>
        </InputSelect>
    </div>

    <button type="submit">Submit</button>
</EditForm>

@code {
    private UserDetails userDetails = new();

    private void HandleSubmit()
    {
        Console.WriteLine(userDetails.UserName);
        Console.WriteLine(userDetails.EmailAddress);
        Console.WriteLine(userDetails.ContactMethod);
    }
}
```

## 12. Submission events you should know

Blazor forms can use different submission events.

### `OnSubmit`

- runs whenever the form is submitted
- does not automatically care whether validation passed

### `OnValidSubmit`

- runs only when the form is valid
- usually the best starting point

### `OnInvalidSubmit`

- runs when the form is submitted but validation fails
- useful if you want custom error behavior

Beginner rule:

- start with `OnValidSubmit`
- add `OnInvalidSubmit` only when you need custom handling

## 13. How to think about form building

When building a form, ask these questions in order:

1. What data do I need from the user?
2. What properties should exist on the model?
3. Which input component fits each property?
4. What validation rules belong on each property?
5. What should happen after valid submission?
6. Should I show all validation errors or field-level errors?

If you answer these six questions first, the form becomes much easier to build.

## 14. Common beginner mistakes

### Forgetting the model

If the `EditForm` has no model, the form has nowhere to store the values.

### Forgetting `DataAnnotationsValidator`

You can write validation attributes on the class, but they will not be enforced unless the form includes `DataAnnotationsValidator`.

### Using the wrong bind syntax

For Blazor input components like `InputText`, use:

```razor
@bind-Value="userDetails.UserName"
```

Do not confuse that with plain HTML input examples that often use `@bind`.

### Submitting without validation intent

If you want the form to process only valid data, prefer `OnValidSubmit` instead of a generic submit flow.

### Mixing too much logic into the markup

Keep the UI in the markup and the processing in the `@code` block.

## 15. How this connects to your recipe app

Your current recipe app already introduces some of the form ideas.

Example from the app:

- the Add Recipe page captures input
- the submit button triggers a method
- the method creates a model object and navigates back home

The next step for that page would be to convert it to a full `EditForm` with:

- a recipe form model
- `InputText` or `InputTextArea`
- validation attributes like `[Required]`
- `DataAnnotationsValidator`
- `ValidationSummary`

That would turn your simple input page into a proper Blazor form component.

## 16. Fast checklist for future form components

When you ask your agent to help with forms later, use this checklist:

1. Create or identify the model class.
2. Add validation attributes if needed.
3. Wrap the inputs in `EditForm`.
4. Bind each input to a model property.
5. Add `DataAnnotationsValidator`.
6. Add `ValidationSummary` or field-level validation UI.
7. Use `OnValidSubmit` for the main submit handler.
8. Keep submit logic in the `@code` block.

## 17. Most important takeaway

The core mechanic of Blazor forms is this:

- inputs bind to a model
- the form submits that model
- validation rules live on the model
- validation components activate and display those rules
- the submit handler processes valid data

Once that pattern is clear, most Blazor forms follow the same structure.