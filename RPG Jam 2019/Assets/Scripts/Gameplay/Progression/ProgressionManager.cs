using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ProgressionManager : MonoBehaviour
{

    public static ProgressionManager i;

    public Dictionary<string, bool> progressionSteps = new Dictionary<string, bool>();

    private Dictionary<string, ProgressionStepListener> stepsEvents = new Dictionary<string, ProgressionStepListener>();//Will permits to notify every listener when a step is reached

    public void completeStep(string stepName) {
        if (progressionSteps.ContainsKey(stepName)){
            progressionSteps[stepName] = true;
            DispatchEvent(stepName);
        }
        else {
            Debug.LogError("Step " + stepName + " is called but does not exists");
        }
    }

    public bool isStepCompleted(string stepName) {
        if (progressionSteps.ContainsKey(stepName)) {
            return progressionSteps[stepName];
        } else {
            Debug.LogError("Step " + stepName + " is called but does not exists");
            return false;
        }
    }

        private void Awake() {
        if (!i) {
            i = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }

        //if we don't load a save
        if (true) {//TODO: know if we load a save or if it is the first game
            foreach (string step in Utility.getProgressionSteps().getSteps()) {
                progressionSteps.Add(step, false);
            }
        }
    }

    public void AddListener(string stepName, ProgressionStepListener.EventHandler eventHandler) {
        ProgressionStepListener invoker;

        if (!stepsEvents.TryGetValue(stepName, out invoker)) {
            invoker = new ProgressionStepListener();
            stepsEvents.Add(stepName, invoker);
        }
        invoker.eventHandler += eventHandler;
    }

    public void RemoveListener(string stepName, ProgressionStepListener.EventHandler eventHandler) {
        ProgressionStepListener invoker;
        if (stepsEvents.TryGetValue(stepName, out invoker)) invoker.eventHandler -= eventHandler;
    }

    public bool HasListener(string stepName) {
        return stepsEvents.ContainsKey(stepName);
    }

    public void DispatchEvent(string stepName, params object[] args) {
        ProgressionStepListener invoker;
        if (stepsEvents.TryGetValue(stepName, out invoker)) {
            EventArgs evt;
            if (args == null || args.Length == 0) {
                evt = new EventArgs(stepName);
            } else {
                evt = new EventArgs(stepName, args);
            }
            invoker.Invoke(evt);
        }
    }
}

//code found on https://developpaper.com/unity-c-custom-event-system/
public class ProgressionStepListener {

    public delegate void EventHandler(EventArgs eventArgs);

    public EventHandler eventHandler;

    /// < summary > Call all added events </summary >
    public void Invoke(EventArgs eventArgs) {
        if (eventHandler != null) eventHandler.Invoke(eventArgs);
    }

    /// < summary > Clean up all event delegations </summary >
    public void Clear() {
        eventHandler = null;
    }
}



public class EventArgs {

    public readonly string type;

    public readonly object[] args;

    public EventArgs(string type) {
        this.type = type;
    }

    public EventArgs(string type, params object[] args) {
        this.type = type;
        this.args = args;
    }

}
