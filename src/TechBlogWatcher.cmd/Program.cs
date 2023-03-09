IReadOnlyList<IBlogParser> parsers = new List<IBlogParser>
{
    new LinkedInBlogParser()
};

IReadOnlyList<INotifier> notifiers = new List<INotifier>
{
    new TelegramNotifier()
};

var monitor = new BlogMonitor(parsers, notifiers);
await monitor.MonitorBlogsAsync();