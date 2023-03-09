namespace TechBlogWatcher.cmd.BlogParsers;

public sealed class LinkedInBlogParser : BlogParserBase, IBlogParser
{
    public BlogInfo BlogInfo { get; }
    public BlogPostXPaths BlogPostXPaths { get; }

    public LinkedInBlogParser()
    {
        BlogInfo = new("https://engineering.linkedin.com/blog", "LinkedIn Engineering Blog");
        BlogPostXPaths = new(
            PostSectionXPath: "/html/body/section[2]/section/div[2]/div[2]/div[2]/div/ul/li/div[2]/div[1]/div[1]",
            TitleXPath: ".//h2/a",
            PostDateXPath: ".//h3/span[@class='timestamp']",
            UrlXPath: ".//h2/a"
        );
    }

    public async Task<IReadOnlyList<BlogPost>> ParseBlogAsync(DateTime lastSentDate) =>
        ParseBlogPostsByXPaths(BlogInfo, BlogPostXPaths, lastSentDate);
}