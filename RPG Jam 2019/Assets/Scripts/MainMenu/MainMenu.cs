using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;


public class MainMenu : MonoBehaviour
{
    public string newGameScene;
    public Canvas canvas;
    public GameObject mainPanel;
    public GameObject loadPanel;
    public GameObject newGamePanel;
    public Button[] loadPanelButtons;

    public InputField saveNameInput;

    private GameObject gameManager;
    private GameObject P;
    private string sPath;

    // Start is called before the first frame update
    void Start()
    {
        sPath = Application.persistentDataPath + "/";
        loadPanelButtons = loadPanel.GetComponentsInChildren<Button>();
        loadPanel.SetActive(false);
        newGamePanel.SetActive(false);

        GameSaves.Instance.FileName = "pimous";

    }

    // Update is called once per frame    
    void Update()
    {

    }


    public void NewGame()
    {
        mainPanel.SetActive(false);
        newGamePanel.SetActive(true);

    }

    public void StartNewGame()
    {
        if (saveNameInput.text == ""){
            GameSaves.Instance.SetGameStatesData("Blank");
        }else{
             GameSaves.Instance.SetGameStatesData(saveNameInput.text);
        }
       
        GameSaves.Instance.gamesStates.entitys.Add(new Character("Bligou"));
        GameSaves.Instance.Save();
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
            loadPanelButtons[i].GetComponentInChildren<Text>().text = tempFilesNames[i].Remove(0, sPath.Length);
        }

    }

    public void LoadGame(Button b)
    {

        //PersistenceManager.Instance.Load(b.GetComponentInChildren<Text>().text);
        GameSaves.Instance.load(b.GetComponentInChildren<Text>().text);
        SceneManager.LoadScene(newGameScene);
    }



    public void Back()
    {
        mainPanel.SetActive(true);
        loadPanel.SetActive(false);
        newGamePanel.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
