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
    public SqliteSaves sqlite;
    void Start()
    {
        //get the active scene name
        Scene scene = SceneManager.GetActiveScene();
        sceneActiveName = scene.name;

        datas = new Dictionary<string, List<Data>>();
        sqlite = new SqliteSaves();

        
        load();

    }

    // Update is called once per frame
    void Update()
    {save();

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
                    if (!datas.ContainsKey(sceneActiveName))
                    {
                        datas.Add(sceneActiveName, new List<Data>());
                    }

                    switch (objectFields[i].GetValue(mono).GetType().ToString())
                    {
                        case "Character":
                            Console.WriteLine("Character");
                            Character tempChar = (Character)objectFields[i].GetValue(mono);
                            List<string> tempInventory = new List<string>();
                            foreach (Item item in tempChar.inventory)
                            {
                             tempInventory.Add(item.ToString().Substring(0,tempChar.armor.ToString().IndexOf('(')-1));
                            }
                            datas[sceneActiveName].Add(new CharacterData(sceneActiveName, mono.name, mono.GetType().ToString(), objectFields[i].Name, objectFields[i].GetValue(mono).ToString(), objectFields[i].GetValue(mono).GetType().ToString(),
                            tempChar.name,tempChar.lvl,tempChar.isAlly,tempChar.currentHealth,tempChar.currentMana,tempInventory,tempChar.armor.ToString().Substring(0,tempChar.armor.ToString().IndexOf('(')-1), tempChar.weapon.ToString().Substring(0,tempChar.weapon.ToString().IndexOf('(')-1),tempChar.gold));
                            break;
                        case "2":
                            
                            break;
                        default:
                            datas[sceneActiveName].Add(new Data(sceneActiveName, mono.name, mono.GetType().ToString(), objectFields[i].Name, objectFields[i].GetValue(mono).ToString(), objectFields[i].GetValue(mono).GetType().ToString()));
                            break;
                    }     

            // Debug.Log("#1#" + mono.GetType() + " #2#" + objectFields[i].Name + " #3#" + objectFields[i].GetValue(mono) + " #4#" + mono.name + " #5#" + objectFields[i].GetValue(mono).GetType());
                }                 //Componenet name         //valueName                 // value
            }
        }
        sqlite.Save(this.datas);

    }

}
