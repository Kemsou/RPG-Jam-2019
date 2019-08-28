using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(StepObject), true)]
public class ProgressionSystemEditor : Editor {

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        StepObject myTarget = (StepObject)target;

        //get all the steps
        List<string> stepsName = new List<string>(Utility.getProgressionSteps().getSteps());

        stepsName.Add("No listened step");
        stepsName.Add("Add new step");

        //display all the steps and let the player select one
        int tmpSelect = EditorGUILayout.Popup("Listened step", Utility.getProgressionSteps().getSteps().IndexOf(myTarget.getListenedStep()), stepsName.ToArray());

        //if "Add new step" is selected
        if (tmpSelect == stepsName.Count - 1) {
            AddStepWindow.showWindow();
        }
        //if "don't listen" is selected
        else if (tmpSelect == stepsName.Count - 2) {
            myTarget.setListenedStep("");
        }
        else{
            //set the listenedStep depending on the selectedIndex
            myTarget.setListenedStep(Utility.getProgressionSteps().getSteps()[tmpSelect]);
            Undo.RegisterUndo(myTarget, "Change listened step");
        }

    }
}

[CustomEditor(typeof(ScriptableStepObject), true)]
public class ProgressionScriptableEditor : Editor {

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        StepObject myTarget = (ScriptableStepObject)target;

        //get all the steps
        List<string> stepsName = new List<string>(Utility.getProgressionSteps().getSteps());

        stepsName.Add("No listened step");
        stepsName.Add("Add new step");

        //display all the steps and let the player select one
        int tmpSelect = EditorGUILayout.Popup("Listened step", Utility.getProgressionSteps().getSteps().IndexOf(myTarget.getListenedStep()), stepsName.ToArray());

        //if "Add new step" is selected
        if (tmpSelect == stepsName.Count - 1) {
            AddStepWindow.showWindow();
        }
        //if "don't listen" is selected
        else if (tmpSelect == stepsName.Count - 2) {
            myTarget.setListenedStep("");
        } else {
            //set the listenedStep depending on the selectedIndex
            myTarget.setListenedStep(Utility.getProgressionSteps().getSteps()[tmpSelect]);
            Undo.RegisterUndo(myTarget, "Change listened step");
        }

    }
}


public class AddStepWindow : EditorWindow {

    string stepName = "";

    [MenuItem("Window/Add Step")]
    static void Init() {
        showWindow();
    }

    public static void showWindow() {
        // Get existing open window or if none, make a new one:
        AddStepWindow window = (AddStepWindow)EditorWindow.GetWindow(typeof(AddStepWindow));
        window.ShowPopup();
    }

    void OnGUI() {
        GUILayout.Label("Add new step", EditorStyles.boldLabel);
        stepName = EditorGUILayout.TextField("Step name", stepName);

        if (GUILayout.Button("Add")) {
            if(stepName == "No listened step" || stepName == "Add new step" || stepName == "") {
                EditorUtility.DisplayDialog("Warning", "You can't have \"No listened step\", \"Add new step\" or an empty string as step name !", "OK");
                stepName = "";
            } else {
                Utility.getProgressionSteps().addProgressionStep(stepName);
                EditorUtility.SetDirty(Utility.getProgressionSteps());
                EditorWindow.GetWindow(typeof(AddStepWindow)).Close();
            }
        }
    }
}
