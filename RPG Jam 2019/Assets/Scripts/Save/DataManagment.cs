using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System;
using UnityEngine.SceneManagement;

public class DataManagment : MonoBehaviour
{
    // Start is called before the first frame update
    public Dictionary<string,List<Data>> datas;
    public string sceneActiveName;
    void Start()
    {
        //get the active scene name
         Scene scene = SceneManager.GetActiveScene();
         sceneActiveName = scene.name;
          Debug.Log("######"+sceneActiveName);

         MonoBehaviour[] sceneActive = FindObjectsOfType<MonoBehaviour>();
        foreach (MonoBehaviour mono in sceneActive)
        {
            FieldInfo[] objectFields = mono.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public);
            for (int i = 0; i < objectFields.Length; i++)
            {
                Savable attribute = Attribute.GetCustomAttribute(objectFields[i], typeof(Savable)) as Savable;
                if (attribute != null)
                 Debug.Log("#1#"+mono.GetType() + " #2#"+objectFields[i].Name + " #3#"+objectFields[i].GetValue(mono)+ " #4#"+mono.name);
            }                 //Componenet name         //valueName                 // value
        }
    }

    // Update is called once per frame
        void Update()
    {
        
    }
}

  public class Data{
        public string sceneName ;
        public string gameObjectName;
        public string componentName;
        public string valueName;
        public string value;
    }
