using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Canvas InventoryCanvas;
    public Canvas pauseMenuCanvas;
    public GameObject player;
    void Start()
    {
        this.InventoryCanvas.enabled = false;
        this.pauseMenuCanvas.enabled = false;

        
        if (GameSaves.Instance.gamesStates.overWorldPositionX != 0.0 && GameSaves.Instance.gamesStates.overWorldPositionY != 0.0 && GameSaves.Instance.gamesStates.overWorldPositionZ != 0.0){
         player.transform.position = new Vector3(GameSaves.Instance.gamesStates.overWorldPositionX, GameSaves.Instance.gamesStates.overWorldPositionY, GameSaves.Instance.gamesStates.overWorldPositionZ);

        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire3"))
        {
            if (this.InventoryCanvas.enabled == false)
            {
                this.InventoryCanvas.enabled = true;
                player.GetComponent<PlayerMovement>().enabled = false;
            }
            else
            {
                this.InventoryCanvas.enabled = false;
                player.GetComponent<PlayerMovement>().enabled = true;
            }

        }
                if (Input.GetButtonDown("start"))
        {
            if (this.pauseMenuCanvas.enabled == false)
            {
                this.pauseMenuCanvas.enabled = true;
                player.GetComponent<PlayerMovement>().enabled = false;
                GameSaves.Instance.gamesStates.overWorldPositionX = player.transform.position.x ;
                GameSaves.Instance.gamesStates.overWorldPositionY = player.transform.position.y ;
                GameSaves.Instance.gamesStates.overWorldPositionZ = player.transform.position.z ;
                GameSaves.Instance.Save();
            }
            else
            {
                this.pauseMenuCanvas.enabled = false;
                player.GetComponent<PlayerMovement>().enabled = true;
            }

        }
    }
}
