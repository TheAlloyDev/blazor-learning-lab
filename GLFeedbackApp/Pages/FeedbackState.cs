namespace GLFeedbackApp.Pages;

public class FeedbackState
{
    private readonly List<Feedback> entries = [];

    public IReadOnlyList<Feedback> Entries => entries;

    public void Add(Feedback feedback)
    {
        entries.Add(new Feedback
        {
            Name = feedback.Name,
            Email = feedback.Email,
            Comment = feedback.Comment
        });
    }
}