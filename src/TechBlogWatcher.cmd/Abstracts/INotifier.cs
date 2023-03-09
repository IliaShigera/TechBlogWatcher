namespace TechBlogWatcher.cmd.Abstracts;

public interface INotifier
{
    Task SendNotificationAsync(string message);
}