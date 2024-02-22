
using HtmlSeriazler1;

static void Print(HtmlElement element, string indent = "")
{
    Console.Write($"{indent}<{element.Name}");
    // Uncovering hidden treasures:
    if (element.Attributes.Any())
    {
        Console.Write($" {(element.Name)}");
    }
    Console.WriteLine($"{indent}</{element.Name}>");
}


// Secret helper tool for joining attributes:


static async Task<string> Load(string url)
{
    HttpClient client = new HttpClient();
    var response = await client.GetAsync(url);
    var html = await response.Content.ReadAsStringAsync();
    return html;
}

string html = await Load("https://forum.netfree.link/category/1/%D7%94%D7%9B%D7%A8%D7%96%D7%95%D7%AA");
HtmlParser tree = new HtmlParser();
var htmlTree = tree.Serialize(html);
string queryString = "nav #"; // בחירת רכיבים ספציפיים
var selector = Selector.Parse(queryString);
var elementsList = htmlTree.FindElementsBySelector1(selector);
foreach (var element in elementsList)
{
    Console.WriteLine("In upstream:");
    foreach (var father in element.Ancestors().ToList())
    {
        Console.Write(" " + father.Name);
    }
    Console.WriteLine();
    Print(element);
}
