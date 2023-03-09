namespace TechBlogWatcher.cmd;

public sealed class BlogMonitor
{
    private readonly IReadOnlyList<IBlogParser> _parsers;
    private readonly IReadOnlyList<INotifier> _notifiers;

    public BlogMonitor(IReadOnlyList<IBlogParser> parsers, IReadOnlyList<INotifier> notifiers)
    {
        _parsers = parsers;
        _notifiers = notifiers;
    }

    public async Task MonitorBlogsAsync()
    {
        var lastSentDate = await LastSentFileAccess.ReadLastSentDateAsync();
        var newPosts = new List<BlogPost>();

        foreach (var parser in _parsers)
        {
            var posts = await parser.ParseBlogAsync(lastSentDate);
            newPosts.AddRange(posts);
        }

        if (newPosts.Any())
        {
            var messageHTML = BuildNotificationMessage(newPosts);

            foreach (var notifier in _notifiers)
                await notifier.SendNotificationAsync(messageHTML);

            await LastSentFileAccess.WriteLastSentDateAsync(DateTime.UtcNow);

            Console.WriteLine("New posts found and sent.");
        }
        else Console.WriteLine("No new posts found.");
    }

    private string BuildNotificationMessage(IReadOnlyList<BlogPost> blogPosts)
    {
        var messageBuilder = new StringBuilder();

        foreach (var post in blogPosts)
        {
            messageBuilder.AppendLine($"{post.BlogName}: <a href=\"{post.Url}\">{post.Title}</a>");
            messageBuilder.AppendLine();
        }

        return messageBuilder.ToString();
    }
}