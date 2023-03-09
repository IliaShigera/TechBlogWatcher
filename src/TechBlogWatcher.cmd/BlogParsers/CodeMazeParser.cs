namespace TechBlogWatcher.cmd.BlogParsers;

public sealed class CodeMazeParser : BlogParserBase, IBlogParser
{
    public BlogInfo BlogInfo { get; }
    public BlogPostXPaths BlogPostXPaths { get; }

    public CodeMazeParser()
    {
        BlogInfo = new("https://code-maze.com/latest-posts-on-code-maze/", "CodeMaze");
        BlogPostXPaths = new(
            PostSectionXPath: "//article",
            TitleXPath: ".//h2[@class='entry-title']//a",
            PostDateXPath: ".//span[@class='published']",
            UrlXPath: ".//h2[@class='entry-title']//a"
        );
    }

    public async Task<IReadOnlyList<BlogPost>> ParseBlogAsync(DateTime lastSentDate) =>
        ParseBlogPostsByXPaths(BlogInfo, BlogPostXPaths, lastSentDate);
}