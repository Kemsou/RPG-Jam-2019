public class Data{
        public string sceneName ;
        public string gameObjectName;
        public string componentName;
        public string valueName;
        public string value;

        public string type;

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
