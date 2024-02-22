using System.IO;
using System.Text.Json;

public class HtmlHelper
{
    private readonly static HtmlHelper _htmlHelper = new HtmlHelper();
    public static HtmlHelper InstanceHtmlHelper => _htmlHelper;
    string allTagsJsonPath = "jsons/HtmlTags.json";
    string selfClosingTagsJsonPath = "jsons/HtmlVoidTags.json";

    public string[] AllHtmlTags { get; private set; }
    public string[] SelfClosingHtmlTags { get; private set; }

    private HtmlHelper()
    {
        AllHtmlTags = LoadTagsFromFile(allTagsJsonPath);
        SelfClosingHtmlTags = LoadTagsFromFile(selfClosingTagsJsonPath);
    }


    private string[] LoadTagsFromFile(string jsonFilePath)
    {
        string jsonContent = File.ReadAllText(jsonFilePath);
        return JsonSerializer.Deserialize<string[]>(jsonContent);
    }
}
