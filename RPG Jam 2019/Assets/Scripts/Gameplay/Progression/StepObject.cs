using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This component must be used for any gameObject using the "progression system", the 
public class StepObject : MonoBehaviour
{
    private VoidDelegate callback;
    [SerializeField][HideInInspector]
    protected string listenedStep = "";

    public void setCallback(VoidDelegate newCallback) {
        callback = newCallback;
    }

    void stepIsReached(EventArgs evt) {
        callback();
    }

    // Start is called before the first frame update
    void Start()
    {
        if(listenedStep != "") {
            initializeListenedStep();
        }

        //If the step is already reached at start (after we load the game. WARNING !!! Load must be done by the save manager before Start() is called, still need to test if Awake() do all of its stuff before Start())
        if (ProgressionManager.i.isStepCompleted(listenedStep)) {
            stepIsReached(null);
        }
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
