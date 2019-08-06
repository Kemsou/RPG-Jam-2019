using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO;


[Serializable]
public sealed class GameSaves  
{
    [NonSerialized()] private static GameSaves instance = null;
    private static readonly object padlock = new object();

    public string FileName { get; set; }
    public GamesStates gamesStates;

    private GameObject gameManager;


    public GameSaves()
    {
        gamesStates = new GamesStates();
    }

    public static GameSaves Instance
    {
        get
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new GameSaves();
                }
                return instance;
            }
        }
        set { }
    }

    void Start()
    {

        gameManager = GameObject.FindWithTag("gameManager");
        //this.Save();
    }


    // Update is called once per frame

    public void Save()
    {
     
       
        FileStream fs = new FileStream(Application.persistentDataPath + "/" + this.FileName, FileMode.Create);

        // Construct a BinaryFormatter and use it to serialize the data to the stream.
        BinaryFormatter formatter = new BinaryFormatter();
        try
        {
            formatter.Serialize(fs, this.gamesStates);
        }
        catch (SerializationException e)
        {
            Console.WriteLine("Failed to serialize. Reason: " + e.Message);
            throw;
        }
        finally
        {
            fs.Close();
        }

    }

    public void load(string FileNameToLoad)
    {
        Debug.Log(Application.persistentDataPath + "/" + FileNameToLoad);
        // Open the file containing the data that you want to deserialize.
        FileStream fs = new FileStream(Application.persistentDataPath + "/" + FileNameToLoad, FileMode.Open);
        try
        {
            BinaryFormatter formatter = new BinaryFormatter();

            this.gamesStates = (GamesStates)formatter.Deserialize(fs);
            this.FileName = this.gamesStates.FileName;

        }
        catch (SerializationException e)
        {
            Console.WriteLine("Failed to deserialize. Reason: " + e.Message);
            throw;
        }
        finally
        {
            fs.Close();
        }
    }


    public void SetGameStatesData()
    {
        this.gamesStates.FileName = this.FileName;

    }

    public void SetGameStatesData(string name)
    {
        this.FileName = name;
        this.gamesStates.FileName = this.FileName;

    }


}
