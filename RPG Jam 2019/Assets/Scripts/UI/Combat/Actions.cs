using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class Actions : MonoBehaviour
{
    public Selectable attackAction;
    public Selectable skipAction;
    public Selectable fleeAction;

    private bool isOpen;

    private Selectable selected;

    private void OnEnable() {

        //Hack to Highlight this motherfucker programatically
        attackAction.Select();
        attackAction.OnSelect(null);

    }

    public void Update() {
        
    }
}
