using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class must be inherited for any behaviour using the "progression system"
public class StepObject : MonoBehaviour
{
    [SerializeField][HideInInspector]
    protected string listenedStep = "";

    // Start is called before the first frame update
    void Start()
    {
        if(listenedStep != "") {
            initializeListenedStep();
        }

        //If the step is already reached at start (after we load the game. WARNING !!! Load must be done before Start() is called)
        if (ProgressionManager.i.isStepCompleted(listenedStep)) {
            stepIsReached(null);
        }
    }

    public virtual void stepIsReached(EventArgs evt) {
        //to be overriden
    }

    public void setListenedStep(string newStep) {
        listenedStep = newStep;
    }

    public void initializeListenedStep() {
        ProgressionManager.i.AddListener(listenedStep, stepIsReached);
    }

    public string getListenedStep() {
        return listenedStep;
    }
}
