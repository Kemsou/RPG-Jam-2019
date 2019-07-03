using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class GameManagerComponent : MonoBehaviour , IPersistence
{
    // Start is called before the first frame update

    public string FileName {get;}
    
    private GameObject gameManager;

    public GameManagerComponent(){
        this.FileName = "papa";
    }

    void Start()
    {
      
        gameManager = GameObject.FindWithTag("gameManager");
        this.Save();
    }

    static bool created = false;
    void Awake()
    {
        if (!created)
        {
            DontDestroyOnLoad(this);
            created = true;
        }
        else
        {
            Destroy(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
       // Debug.Log(Application.persistentDataPath);
    }

    public void Save()
    {
        PersistenceManager P = new PersistenceManager();
        P.Save(this);
    }

}
