namespace TechBlogWatcher.cmd.Extensions;

public static class WebElementExtensions
{
    public static string ParseTextByXPath(this IWebElement webElement, string xPath, string attribute = default)
    {
        var newElement = webElement.FindElement(By.XPath(xPath));

        if (string.IsNullOrWhiteSpace(attribute))
            return newElement.GetAttribute(attribute);

        return newElement.Text;
    }

    public static DateTime ParseDateByXPath(this IWebElement webElement, string xPath)
    {
        var dateString = webElement.FindElement(By.XPath(xPath)).Text;
        
        // modifies the date string by removing the ordinal suffixes "st", "nd", "rd", and "th" from the day of the month component.
        dateString = dateString.Replace("st", "").Replace("nd", "").Replace("rd", "").Replace("th", "");

        DateTime date;

        if (!DateTime.TryParse(dateString, out date))
        {
            if (!DateTime.TryParseExact(dateString, "dddd d\\t\\h MMMM yyyy", null, DateTimeStyles.None, out date))
            {
                DateTime.TryParseExact(dateString, "dddd d MMMM yyyy", null, DateTimeStyles.None, out date);
            }
        }

        return date;
    }
}