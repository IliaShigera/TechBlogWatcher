IReadOnlyList<IBlogParser> parsers = new List<IBlogParser>
{
};

IReadOnlyList<INotifier> notifiers = new List<INotifier>
{
};

var monitor = new BlogMonitor(parsers, notifiers);
await monitor.MonitorBlogsAsync();