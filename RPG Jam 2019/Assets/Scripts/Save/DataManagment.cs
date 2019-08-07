using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System;
using UnityEngine.SceneManagement;

public class DataManagment : MonoBehaviour
{
    // Start is called before the first frame update
    public Dictionary<string, List<Data>> datas;
    public string sceneActiveName;
    public  SqliteSaves  sqlite;
    void Start()
    {
        //get the active scene name
        Scene scene = SceneManager.GetActiveScene();
        sceneActiveName = scene.name;
        Debug.Log("######" + sceneActiveName);

        datas = new Dictionary<string, List<Data>>();
        sqlite = new SqliteSaves();

        save();
        load();

    }

    // Update is called once per frame
    void Update()
    {

    }

    void load()
    {
        this.datas = sqlite.GetDatas();
        Debug.Log(this.datas.Count);
    }

    void save()
    {
        MonoBehaviour[] sceneActive = FindObjectsOfType<MonoBehaviour>();
        foreach (MonoBehaviour mono in sceneActive)
        {
            FieldInfo[] objectFields = mono.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public);
            for (int i = 0; i < objectFields.Length; i++)
            {
                Savable attribute = Attribute.GetCustomAttribute(objectFields[i], typeof(Savable)) as Savable;
                if (attribute != null)
                {

                    if (datas.ContainsKey(sceneActiveName))
                    {

                        datas[sceneActiveName].Add(new Data(sceneActiveName, mono.name, mono.GetType().ToString(), objectFields[i].Name, objectFields[i].GetValue(mono).ToString(), objectFields[i].GetValue(mono).GetType().ToString()));
                    }
                    else
                    {
                        datas.Add(sceneActiveName, new List<Data>());
                        datas[sceneActiveName].Add(new Data(sceneActiveName, mono.name, mono.GetType().ToString(), objectFields[i].Name, objectFields[i].GetValue(mono).ToString(), objectFields[i].GetValue(mono).GetType().ToString()));
                    }

                    Debug.Log("#1#" + mono.GetType() + " #2#" + objectFields[i].Name + " #3#" + objectFields[i].GetValue(mono) + " #4#" + mono.name + " #5#" + objectFields[i].GetValue(mono).GetType());
                }                 //Componenet name         //valueName                 // value
            }
        }
    sqlite.Save(this.datas);
        
    }
}
