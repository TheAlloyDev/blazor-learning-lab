using GLFeedbackApp.Models;
using GLFeedbackApp.Storage;

namespace GLFeedbackApp.Services;

public class FeedbackService
{
	private readonly FeedbackStorage feedbackStorage;

	public FeedbackService(FeedbackStorage feedbackStorage)
	{
		this.feedbackStorage = feedbackStorage;
	}

	// Loads the saved list, appends the new item, and saves the updated list.
	public async Task AddFeedbackAsync(Feedback feedback)
	{
		var feedbackList = await feedbackStorage.LoadFeedbackAsync();
		feedbackList.Add(feedback);
		await feedbackStorage.SaveFeedbackAsync(feedbackList);
	}

	// Returns the current saved feedback entries from local storage.
	public Task<List<Feedback>> GetFeedbackAsync()
	{
		return feedbackStorage.LoadFeedbackAsync();
	}
}