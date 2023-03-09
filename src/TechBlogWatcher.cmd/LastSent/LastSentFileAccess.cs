namespace TechBlogWatcher.cmd.LastSent;

public static class LastSentFileAccess
{
    private static readonly string FilePath;

    static LastSentFileAccess() => FilePath = string.Empty;

    /// <summary>
    /// Read last sent date from file, or use default value if file doesn't exist
    /// </summary>
    public static async Task<DateTime> ReadLastSentDateAsync()
    {
        DateTime lastSentDate;

        if (File.Exists(FilePath))
        {
            var lastSentDateString = await File.ReadAllTextAsync(FilePath);
            if (!string.IsNullOrWhiteSpace(lastSentDateString))
                lastSentDate = DateTime.Parse(lastSentDateString);
            else
                lastSentDate = DateTime.UtcNow.AddDays(-1);
        }
        else
        {
            lastSentDate = DateTime.UtcNow.AddDays(-1);
            // create new file and write init date
            await using var fs = File.OpenWrite(FilePath);
            var bytes = Encoding.UTF8.GetBytes(lastSentDate.ToString("o"));
            await fs.WriteAsync(bytes);
        }

        return lastSentDate;
    }

    public static async Task WriteLastSentDateAsync(DateTime lastSentDate) =>
        await File.WriteAllTextAsync(FilePath, DateTime.UtcNow.ToString("o"));
}