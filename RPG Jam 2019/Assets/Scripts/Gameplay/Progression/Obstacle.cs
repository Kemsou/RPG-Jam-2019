using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//This component is used for GameObjects that disappears when a certain condition is met
public class Obstacle : StepObject
{
    private void Start() {
        setCallback(removeObstacle);
    }

    public void removeObstacle() {

        Debug.Log(gameObject.name +" removed");

        Destroy(this);
    }
}
