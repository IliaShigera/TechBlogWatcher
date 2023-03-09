namespace TechBlogWatcher.cmd.Notifiers;

public interface INotifier
{
    Task SendNotificationAsync(string message);
}