using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionScript : MonoBehaviour
{
    public DialogueTrigger trigger;
    // Start is called before the first frame update
    private Collision2D col;
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "player")
        {
            col = collision;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "player")
        {
            col = null;
        }
    }

    void Update()
    {

        if (Input.GetKeyDown("e") && col != null)
        {
            this.trigger.TriggerDialogue();
        }

    }
}
