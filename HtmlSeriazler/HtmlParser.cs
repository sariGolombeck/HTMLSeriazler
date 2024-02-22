  
using HtmlSeriazler1;
using System.Text.RegularExpressions;
using HtmlSeriazler1;
namespace HtmlSeriazler1
{
    public class HtmlParser
    {
        private HtmlHelper htmlHelper;

        public HtmlParser()
        {
            htmlHelper = HtmlHelper.InstanceHtmlHelper;
        }
        public  HtmlElement Serialize(string html)
        {
            // ניקוי רווחים מיותרים
            html = Regex.Replace(html, "\\s+", " ");

            // פיצול מחרוזת ה-HTML לשורות
            var lines = Regex.Split(html, "<(.*?)>").Where(s => s.Length > 0).ToList();

            // יצירת אלמנט שורש
            var root = new HtmlElement();

            // אלמנט נוכחי
            var currentElement = root;

            foreach (var line in lines)
            {
                // פיצול השורה למילה הראשונה והשאר
                var parts = line.Split(' ', 2);
                var firstWord = parts[0];
                var restOfString = parts.Length > 1 ? parts[1] : "";

                // טיפול בתגיות סוגרות
                if (firstWord.StartsWith("/"))
                {
                    currentElement = currentElement.Parent;
                    continue;
                }

                // טיפול בתגיות פותחות
                if (HtmlHelper.InstanceHtmlHelper.AllHtmlTags.Contains(firstWord))
                {
                    var newElement = new HtmlElement
                    {
                        Name = firstWord
                    };

                    ParseAttributes(restOfString, newElement);

                    newElement.Parent = currentElement;
                    currentElement.Children.Add(newElement);

                    if (line.EndsWith("/") || HtmlHelper.InstanceHtmlHelper.SelfClosingHtmlTags.Contains(firstWord))
                    {
                        currentElement = newElement.Parent;
                    }
                    else
                    {
                        currentElement = newElement;
                    }
                }
                else
                {
                    // תוכן פנימי
                    currentElement.InnerHtml = line;
                }
            }

            // החזרת אלמנט השורש
            return root;
        }
           
            private void ParseAttributes(string attributesString, HtmlElement element)
        {
            // התאמת תכונות
            var matches = Regex.Matches(attributesString, "([a-zA-Z]+)=\\\"([^\\\"]*)\\\"");

            foreach (Match match in matches)
            {
                var attributeName = match.Groups[1].Value;
                var attributeValue = match.Groups[2].Value;

                element.Attributes.Add($"{attributeName}=\"{attributeValue}\"");

                // טיפול בתכונה class
                if (attributeName == "class")
                {
                    element.Classes.AddRange(attributeValue.Split(' '));
                }

                // טיפול בתכונה id
                if (attributeName == "id")
                {
                    element.Id = attributeValue;
                }
            }
        }
    }
}