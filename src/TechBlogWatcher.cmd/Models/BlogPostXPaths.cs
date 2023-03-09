namespace TechBlogWatcher.cmd.Models;

public sealed record BlogPostXPaths(
    string PostSectionXPath,
    string TitleXPath,
    string PostDateXPath,
    string UrlXPath
);