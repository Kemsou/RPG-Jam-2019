using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "Steps", menuName = "Progression Steps")]
public class Steps : ScriptableObject
{
    public List<string> progressionSteps = new List<string>();

    //If we already have a step with this name, we don't add it
    public void addProgressionStep(string stepName) {
        if (stepName == "") {
            EditorUtility.DisplayDialog("Warning", "You can't add a step with an empty name", "OK");
            return;
        }
        if (!progressionSteps.Contains(stepName)) {
            progressionSteps.Add(stepName);
        } else {
            EditorUtility.DisplayDialog("Warning", "Step " + stepName + " already exists", "OK");
        }
    }

    public List<string> getSteps() {
        return progressionSteps;
    }
}
