namespace TechBlogWatcher.cmd.Abstracts;

public abstract class BlogParserBase
{
    protected const int DefaultTimeoutSeconds = 30;

    protected virtual ChromeOptions GetChromeOptions()
    {
        var options = new ChromeOptions();
        options.AddArguments("--headless");
        options.AddArguments("--disable-gpu");
        options.AddArguments("--disable-dev-shm-usage");
        options.AddArguments("--no-sandbox");
        options.AddArguments("--disable-extensions");
        return options;
    }

    protected virtual void NavigateToUrl(ChromeDriver driver, string url) => driver.Navigate().GoToUrl(url);

    protected virtual IReadOnlyCollection<IWebElement> WaitForNodes(ChromeDriver driver, By locator,
        int timeoutSeconds = DefaultTimeoutSeconds)
    {
        var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutSeconds));
        return wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(locator));
    }

    protected virtual DateTime ParseDateString(string dateString)
    {
        DateTime date;

        // modifies the date string by removing the ordinal suffixes "st", "nd", "rd", and "th" from the day of the month component.
        dateString = dateString.Replace("st", "").Replace("nd", "").Replace("rd", "").Replace("th", "");

        if (!DateTime.TryParse(dateString, out date))
        {
            if (!DateTime.TryParseExact(dateString, "dddd d\\t\\h MMMM yyyy", null, DateTimeStyles.None, out date))
            {
                DateTime.TryParseExact(dateString, "dddd d MMMM yyyy", null, DateTimeStyles.None, out date);
            }
        }

        return date;
    }

    protected IReadOnlyList<BlogPost> ParseBlogPostsByXPaths(BlogInfo blogInfo, BlogPostXPaths xPaths, DateTime lastSentDate)
    {
        var newPosts = new List<BlogPost>();
        
        try
        {
            var options = GetChromeOptions();
            using var driver = new ChromeDriver(options);
            NavigateToUrl(driver, blogInfo.Url);
            var postNodes = WaitForNodes(driver, By.XPath(xPaths.PostSectionXPath));

            foreach (var postNode in postNodes)
            {
                var postDate = postNode.ParseDateByXPath(xPaths.PostDateXPath);
                if (postDate < lastSentDate) continue;
                var postTitle = postNode.ParseTextByXPath(xPaths.TitleXPath);
                var postUrl = postNode.ParseTextByXPath(xPaths.UrlXHath, xPaths.Attribute);
                var post = new BlogPost(blogInfo.Name, postTitle, postUrl);
                newPosts.Add(post);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error parsing {blogInfo.Url}: {ex.Message}");
        }

        return newPosts.AsReadOnly();
    }
}