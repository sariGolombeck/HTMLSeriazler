
using HtmlSeriazler1;
using System.Collections.Generic;

namespace HtmlSeriazler1
{
    public static class HtmlElementExtensions
    {
        public static HashSet<HtmlElement> FindElementsBySelector1(this HtmlElement element, Selector selector)
        {
            HashSet<HtmlElement> matchingElements = new HashSet<HtmlElement>();
            FindElementsRecursively(element, selector, matchingElements);
            return matchingElements;
        }

        private static void FindElementsRecursively(HtmlElement element, Selector selector, HashSet<HtmlElement> matchingElements)
        {
            // Check for termination condition (last selector reached)
            if (selector.Child == null)
            {
                // Add elements matching the current selector to the result
                if (MatchesSelector(element, selector))
                {
                    matchingElements.Add(element);
                }
                return;
            }

            // Recursively traverse descendants matching the current selector
            foreach (HtmlElement descendant in element.Descendants())
            {
                if (MatchesSelector(descendant, selector))
                {
                    FindElementsRecursively(descendant, selector.Child, matchingElements);
                }
            }
        }

        private static bool MatchesSelector(HtmlElement element, Selector selector)
        {
            return (string.IsNullOrEmpty(selector.TagName) || selector.TagName == element.Name) &&
                   (string.IsNullOrEmpty(selector.Id) || selector.Id == element.Id) &&
                   (selector.Classes.All(element.Classes.Contains));
        }
        public static int CountElements(this HtmlElement element)
        {
            return 1 + element.Children.Sum(child => child.CountElements());
        }
    }
}
