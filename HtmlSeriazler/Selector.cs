
using System.Text.RegularExpressions;

public class Selector
{
    public string TagName { get; set; }
    public string Id { get; set; }
    public List<string> Classes { get; set; }
    public Selector Parent { get; set; }
    public Selector Child { get; set; }

    public Selector()
    {
        Classes = new List<string>();
    }
    public static Selector Parse(string selectorString)
    {
        // Split the selector string into parts by spaces.
        var parts = selectorString.Split(' ');

        // Create the root object and a placeholder for the current selector.
        var root = new Selector();
        var current = root;

        // Iterate over the parts and build the selector tree.
        foreach (var part in parts)
        {
            // Split the part into parts by the # and . separators.
            var subparts = Regex.Matches(part, @"([^\.#]+)|#([^.]+)|(\.[^.]+)").Cast<Match>().Select(m => m.Value).ToList();

            // Update the current selector's properties with the parts.
            foreach (var subpart in subparts)
            {
                if (subpart.StartsWith('#'))
                {
                    current.Id = subpart.Substring(1);
                }
                else if (subpart.StartsWith('.'))
                {
                    current.Classes.Add(subpart.Substring(1));
                }
                else if (HtmlHelper.InstanceHtmlHelper.AllHtmlTags.Contains(subpart))
                {
                    current.TagName = subpart;
                }
            }

            // Create a new selector object, add it as a child of the current selector, and update the current selector to point to it.
            var child = new Selector();
            current.Child = child;
            current = child;
        }

        return root;
    }
}