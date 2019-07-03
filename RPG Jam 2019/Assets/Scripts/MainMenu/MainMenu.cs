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
    // Start is called before the first frame update
    void Start()
    {
        
        canvas = GetComponent<Canvas>();
        mainPanel = GameObject.Find("MainPanel");
        loadPanel = GameObject.Find("LoadPanel");
        loadPanel.SetActive(false);

    }

    // Update is called once per frame    
    void Update()
    {

    }


    public void NewGame()
    {
        SceneManager.LoadScene(newGameScene);
    }

    public void LoadGame()
    {
        mainPanel.SetActive(false);
        loadPanel.SetActive(true);
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
