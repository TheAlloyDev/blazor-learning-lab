using Microsoft.JSInterop;

namespace GLFeedbackApp.Pages;

// Handles saving and loading feedback data from the browser's local storage.
public class FeedbackState
{
    private readonly IJSRuntime jsRuntime;

    // IJSRuntime lets this C# service call browser JavaScript APIs.
    public FeedbackState(IJSRuntime jsRuntime)
    {
        this.jsRuntime = jsRuntime;
    }

    // Converts the feedback list to JSON and stores it in local storage.
    public async Task SaveFeedbackAsync(List<Feedback> feedbackList)
    {
        var json = System.Text.Json.JsonSerializer.Serialize(feedbackList);
        await jsRuntime.InvokeVoidAsync("localStorage.setItem", "feedback", json);
    }

    // Reads the JSON string from local storage and converts it back into feedback objects.
    public async Task<List<Feedback>> LoadFeedbackAsync()
    {
        var json = await jsRuntime.InvokeAsync<string>("localStorage.getItem", "feedback");
        if (string.IsNullOrEmpty(json))
        {
            return new List<Feedback>();
        }

        var feedbackList = System.Text.Json.JsonSerializer.Deserialize<List<Feedback>>(json);
        return feedbackList ?? new List<Feedback>();
    }

}