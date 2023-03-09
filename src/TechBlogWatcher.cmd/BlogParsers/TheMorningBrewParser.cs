namespace TechBlogWatcher.cmd.BlogParsers;

public sealed class TheMorningBrewParser : BlogParserBase, IBlogParser
{
    public BlogInfo BlogInfo { get; }
    public BlogPostXPaths BlogPostXPaths { get; }

    public TheMorningBrewParser()
    {
        BlogInfo = new("https://blog.cwa.me.uk/", "The Morning Brew");
        BlogPostXPaths = new(
            PostSectionXPath: "//div[@id='content']//following-sibling::div[@class='post']",
            TitleXPath: ".//h2[@class='post-title']//a",
            PostDateXPath: ".//p[@class='day-date']//em[2]",
            UrlXPath: ".//h2[@class='post-title']//a"
        );
    }

    public async Task<IReadOnlyList<BlogPost>> ParseBlogAsync(DateTime lastSentDate) =>
        ParseBlogPostsByXPaths(BlogInfo, BlogPostXPaths, lastSentDate);
}