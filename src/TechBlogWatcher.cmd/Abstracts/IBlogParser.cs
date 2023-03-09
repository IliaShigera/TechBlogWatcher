namespace TechBlogWatcher.cmd.Abstracts;

public interface IBlogParser
{
    BlogInfo BlogInfo { get; }
    BlogPostXPaths PostXPaths { get; }
    
    Task<IReadOnlyList<BlogPost>> ParseBlogAsync(DateTime lastSentDate);
}