using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MainMenu : MonoBehaviour
{
    public string newGameScene;
    public Canvas canvas;
    public GameObject mainPanel;
    public GameObject loadPanel;
    public Button[] loadPanelButtons;

    private GameObject gameManager;
    private GameObject P;
    private PersistenceManager pComponent;
    private string  sPath ;
    
    // Start is called before the first frame update
    void Start()
    {
        sPath = Application.persistentDataPath + "/";
        canvas = GetComponent<Canvas>();
        mainPanel = GameObject.Find("MainPanel");
        loadPanel = GameObject.Find("LoadPanel");
        loadPanelButtons = loadPanel.GetComponentsInChildren<Button>();
        loadPanel.SetActive(false);

         gameManager = GameObject.FindWithTag("gameManager");
         if (gameManager == null){
            gameManager = new GameObject("GameManager");
         }
    }

    // Update is called once per frame    
    void Update()
    {

    }


    public void NewGame()
    {
        gameManager.AddComponent<GameManagerComponent>();
        SceneManager.LoadScene(newGameScene);
        
    }

    public void LoadGamePanel()
    {
        mainPanel.SetActive(false);
        loadPanel.SetActive(true);
        
        List<string> tempFilesNames = new List<string>();
        foreach (string sFileName in System.IO.Directory.GetFiles(sPath))
        {
           tempFilesNames.Add(sFileName);
        }
       
        for (int i = 0; i < tempFilesNames.Count; i++)
        {
            loadPanelButtons[i].GetComponentInChildren<Text>().text = tempFilesNames[i].Remove(0,sPath.Length);
        }
        
    }

    public void LoadGame(Button b)
    {
        P = new GameObject("persitance");
        P.AddComponent<PersistenceManager>();
        pComponent = P.GetComponent<PersistenceManager>();
        //GameManagerComponent t = (GameManagerComponent)(P.Load(b.GetComponentInChildren<Text>().text));
        Debug.Log(pComponent.Load(b.GetComponentInChildren<Text>().text));
        //gameManager.AddComponent<GameManagerComponent>();
        SceneManager.LoadScene(newGameScene);
    }



    public void Back()
    {
        mainPanel.SetActive(true);
        loadPanel.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
