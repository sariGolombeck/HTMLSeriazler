
namespace HtmlSeriazler1
{

    using System;
    using System.Collections.Generic;

    public class HtmlElement
    {
        // תכונות
        public string Id { get; set; }
        public string Name { get; set; }
        public List<string> Attributes { get; set; }
        public List<string> Classes { get; set; }
        public string InnerHtml { get; set; }

        // קשרי הוראה
        public HtmlElement Parent { get; set; }
        public List<HtmlElement> Children { get; set; }

        // קונסטרקטור
        public HtmlElement()
        {
            Attributes = new List<string>();
            Classes = new List<string>();
            Children = new List<HtmlElement>();
        }


        public IEnumerable<HtmlElement> Descendants()
        {
            Queue<HtmlElement> queue = new Queue<HtmlElement>();
            queue.Enqueue(this);

            while (queue.Any())
            {
                HtmlElement current = queue.Dequeue();
                foreach (HtmlElement child in current.Children)
                {
                    queue.Enqueue(child);
                }
                yield return current;
            }
        }


        public IEnumerable<HtmlElement> Ancestors()
        {
            HtmlElement htmlElement = this;
            while (htmlElement != null)
            {
                yield return htmlElement;
                htmlElement = htmlElement.Parent;
            }
        }
    

}
}