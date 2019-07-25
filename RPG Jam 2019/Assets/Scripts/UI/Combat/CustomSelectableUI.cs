using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public enum ChosableAction { Attack, Skill, Skip, Flee }

public class CustomSelectableUI : MonoBehaviour
{
    public enum UIState { Selected, NotSelected, Disable }

    private float timeBeforeChangingSelection = 0.3f;
    private float timer = 0f;

    [SerializeField]
    public ChosableAction action;

    public Sprite arrowImage;

    public bool isSelectedAtStart;

    public CustomSelectableUI up;
    public CustomSelectableUI right;
    public CustomSelectableUI down;
    public CustomSelectableUI left;

    public Color selectedColor = Color.yellow;
    public Color unselectedColor = Color.white;
    public Color disabledColor = Color.grey;

    public UIState currentUIState = UIState.NotSelected;
    private Text text;

    CombatUIManager combatUIManager;

    private void Awake() {
        text = GetComponent<Text>();
    }

    private void Start() {
        combatUIManager = GameObject.FindGameObjectWithTag("combatMenu").GetComponent<CombatUIManager>();
    }

    private void OnEnable() {
        timer = 0;
        if (isSelectedAtStart) {
            changeState(UIState.Selected);
        } else {
            changeState(UIState.NotSelected);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (currentUIState == UIState.Selected) {
            timer += Time.deltaTime;
            if (timer >= timeBeforeChangingSelection) {
                if (Input.GetAxisRaw("Horizontal") > 0 && right) {
                    selectNewItem(right);
                    return;
                }
                if (Input.GetAxisRaw("Horizontal") < 0 && left) {
                    selectNewItem(left);
                    return;
                }
                if (Input.GetAxisRaw("Vertical") > 0 && up) {
                    selectNewItem(up);
                    return;
                }
                if (Input.GetAxisRaw("Vertical") < 0 && down) {
                    selectNewItem(down);
                    return;
                }

                if (Input.GetButton("Submit")) {
                    combatUIManager.actionChosen(action);
                }
            }
        }
    }

    private void selectNewItem(CustomSelectableUI item) {
        changeState(UIState.NotSelected);
        item.changeState(UIState.Selected);
    }

    public void changeState(UIState newState) {
        currentUIState = newState;
        timer = 0;
        if (currentUIState == UIState.Selected) {
            text.color = selectedColor;
        }
        if (currentUIState == UIState.NotSelected) {
            text.color = unselectedColor;
        }
        if (currentUIState == UIState.Disable) {
            text.color = disabledColor;
        }
    }
}
