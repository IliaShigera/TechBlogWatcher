IReadOnlyList<IBlogParser> parsers = new List<IBlogParser>
{
    new LinkedInBlogParser(),
    new CodeMazeParser(),
    new TheMorningBrewParser()
};

IReadOnlyList<INotifier> notifiers = new List<INotifier>
{
    new TelegramNotifier()
};

var monitor = new BlogMonitor(parsers, notifiers);
await monitor.MonitorBlogsAsync();