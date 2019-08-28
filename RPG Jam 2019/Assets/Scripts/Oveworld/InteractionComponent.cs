using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This component must be put on a gameObject that can be interacted with
public class InteractionComponent : MonoBehaviour
{
    private Collision2D col;
    private VoidDelegate callback;

    public void setCallback(VoidDelegate newCallback) {
        callback = newCallback;
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "player") {
            col = collision;
        }
    }

    void OnCollisionExit2D(Collision2D collision) {
        if (collision.gameObject.tag == "player") {
            col = null;
        }
    }

    void Update() {
        if (Input.GetButtonDown("Fire1") && col != null) {
            callback();
        }

    }
}
