namespace TechBlogWatcher.cmd.Abstracts;

public interface IBlogParser
{
    BlogInfo BlogInfo { get; }
    BlogPostXPaths BlogPostXPaths { get; }
    
    Task<IReadOnlyList<BlogPost>> ParseBlogAsync(DateTime lastSentDate);
}