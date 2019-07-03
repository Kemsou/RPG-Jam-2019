using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour , IPersistence
{
    // Start is called before the first frame update

    public string FileName {get;}

    public GameManager(){
        this.FileName = "papa";
    }

    void Start()
    {
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
        //Debug.Log(Application.persistentDataPath);
    }

    public void Save()
    {
        PersistenceManager P = new PersistenceManager();
        P.Save(this);
    }

    public void Load(string saveName){
        PersistenceManager P = new PersistenceManager();
        P.Load(saveName);
    }
}
