public class Data
{
    public string sceneName { get; set; }
    public string gameObjectName { get; set; }
    public string componentName { get; set; }
    public string valueName { get; set; }
    public string value { get; set; }

    public string type { get; set; }

    public Data(string sceneName, string gameObjectName, string componentName, string valueName, string value, string type)
    {
        this.sceneName = sceneName;
        this.gameObjectName = gameObjectName;
        this.componentName = componentName;
        this.valueName = valueName;
        this.value = value;
        this.type = type;
    }
}
