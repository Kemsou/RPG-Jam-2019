using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//TODO replace this placeholder by automated gauge creation at start
[System.Serializable]
public class PlaceHolderGaugeListe {
    public string chara;
    public Slider gauge;
}

public class CombatUIManager : MonoBehaviour {
    public List<PlaceHolderGaugeListe> gaugeList;
    public bool debug;
    public Image actionMenu;

    private Queue<string> characterWaitingToChooseAction = new Queue<string>(); //every player character finishing its ATB timer is put in this list to be handled later
    private bool canOpenMenu = true;

    private string characterInMenu = "";

    public bool isATBTimeFlowing = true;

    public Color allyFillingATBGaugeColor = Color.yellow;
    public Color allyFullATBGaugeColor = Color.green;
    public Color enemyFillingATBGaugeColor = Color.red;
    public Color enemyFullATBGaugeColor = Color.black;

    private CombatManager combatManager;

    // Start is called before the first frame update
    void Start() {
        combatManager = CombatManager.i;
        combatManager.initializeCombat(this);
    }

    // Update is called once per frame
    void Update() {
        //if we don't have any acting character, we try to find one
        if (characterInMenu == "" && characterWaitingToChooseAction.Count > 0) {
            characterInMenu = characterWaitingToChooseAction.Dequeue();
        }

        //If we have an character in menu
        if (characterInMenu != "" && canOpenMenu) {
            //if it is an ally and the action menu is not yet open, we open it
            openActingMenu();
        }
        if (isATBTimeFlowing) {
            foreach (Character chara in combatManager.characters) {
                updateATBChar(chara);
            }
        }  
    }

    //TODO: bug; the ally stays the current acting character while the menu is open

    public void updateATBChar(Character character) {
        Slider sliderATB = getCharaATBGauge(character.name);
        sliderATB.value += (Time.deltaTime + (float)combatManager.getCharacterFromName(character.name).getTotalCharacWithoutBuff(Characteristic.Speed) / 1000) / 3;//formula to calculate character's speed;
        if (sliderATB.value >= 1) {
            //the ATB is now permitting the character to act (its gauge is filled)
            if (character.isAlly || debug)
                sliderATB.transform.Find("Fill Area").gameObject.GetComponentInChildren<Image>().color = character.isAlly ? allyFullATBGaugeColor : enemyFullATBGaugeColor;

            //if this character is an ally, we add it to the action menu character queue
            if (character.isAlly && !characterWaitingToChooseAction.Contains(character.name) && characterInMenu != character.name)
                characterWaitingToChooseAction.Enqueue(character.name);

            //if this character is an enemy, it acts directly
            if (!character.isAlly) {
                enemyAct(character.name);
            }
        }else if(character.isAlly || debug) {
            sliderATB.transform.Find("Fill Area").gameObject.GetComponentInChildren<Image>().color = character.isAlly ? allyFillingATBGaugeColor : enemyFillingATBGaugeColor;
        }

    }

    private Slider getCharaATBGauge(string chara) {
        foreach (PlaceHolderGaugeListe e in gaugeList) {
            if (e.chara == chara) {
                return e.gauge;
            }
        }
        Debug.Log("WARNING: " + chara + " not found in gauge list");
        return null;
    }

    private void openActingMenu() {
        //we open the menu for the player to choose the action
        actionMenu.gameObject.SetActive(true);
        canOpenMenu = false;
    }

    //function called by the chosen action
    public void actionChosen(ChosableAction action) {
        switch (action) {
            case ChosableAction.Attack:
                //TODO: the player must be able to choose its target
                combatManager.makeCharacterAct(characterInMenu, combatManager.getCharacterFromName(characterInMenu).weapon, new List<Character> { combatManager.getRandomEnemy()});
                break;
            case ChosableAction.Skip:
                break;
            case ChosableAction.Flee:
                Debug.Log("Flee not implemented yet !"); //TODO code the flee feature
                break;
            case ChosableAction.Skill:
                Debug.LogError("Wrong function called. you need to call the actionChosen(ChosableAction, string) for a Skill");
                break;
            default:
                Debug.LogError("Not treated action: " + action);
                break;
        }
        actionSelected();

    }

    public void actionChosen(ChosableAction action, string chosenSkill) {
        if (action != ChosableAction.Skill) {
            Debug.LogError("Wrong function called. you need to call the actionChosen(ChosableAction) for a non Skill action");
            return;
        }
        Debug.Log("Skill not implemented yet !"); //TODO code the Skill feature
        actionSelected();
    }



    private void actionSelected() {
        getCharaATBGauge(characterInMenu).value = 0;
        characterInMenu = "";
        actionMenu.gameObject.SetActive(false);
        canOpenMenu = true;
    }
    

    private void enemyAct(string enemyName) {
        getCharaATBGauge(enemyName).value = 0;
        combatManager.enemyChooseAction(enemyName);
    }

}
