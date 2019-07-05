using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Canvas canvas;
    void Start()
    {
        this.canvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("i"))
        {
            if (this.canvas.enabled == false)
            {
                this.canvas.enabled = true;
            }
            else
            {
                this.canvas.enabled = false;
            }

        }
    }
}
