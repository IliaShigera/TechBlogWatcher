namespace TechBlogWatcher.cmd.Notifiers.Telegram;

public sealed class TelegramNotifier : INotifier
{
    public async Task SendNotificationAsync(string message)
    {
        var telegramBotClient = new TelegramBotClient(TelegramConfiguration.BotToken);
        await telegramBotClient.SendTextMessageAsync(TelegramConfiguration.ChannelId, message, ParseMode.Html);
    }
}