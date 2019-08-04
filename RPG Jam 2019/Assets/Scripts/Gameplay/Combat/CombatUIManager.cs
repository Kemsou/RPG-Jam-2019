using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CombatUIManager : MonoBehaviour {
    public List<GameObject> alliesGauges;
    public List<GameObject> enemiesGauges;

    public bool debug;
    public Image actionMenu;

    public bool isATBTimeFlowing = true;

    public Color allyFillingATBGaugeColor = Color.yellow;
    public Color allyFullATBGaugeColor = Color.green;
    public Color enemyFillingATBGaugeColor = Color.red;
    public Color enemyFullATBGaugeColor = Color.black;

    public AnimationCurve looseHealthSpeedAnimation;

    private CombatManager combatManager;

    private Queue<string> characterWaitingToChooseAction = new Queue<string>(); //every player character finishing its ATB timer is put in this list to be handled later
    private bool canOpenMenu = true;

    private string characterInMenu = "";

    private Dictionary<string, Slider> atbGauges = new Dictionary<string, Slider>();
    private Dictionary<string, Slider> healthGauges = new Dictionary<string, Slider>();
    private Dictionary<string, Slider> bufferHealthGauges = new Dictionary<string, Slider>();

    private List<CharacterCombat> charactersCombat = new List<CharacterCombat>();

    // Start is called before the first frame update
    void Start() {
        combatManager = CombatManager.i;
        combatManager.initializeCombat(this);

        initializeUI();
        initializeCharactersGraphics();

    }

    private void initializeUI() {
        initializeGauges(combatManager.getAlliesList(), alliesGauges);
        if(debug)
            initializeGauges(combatManager.getEnemiesList(), enemiesGauges);

        foreach(Character chara in combatManager.characters) {
            healthGauges[chara.name].value = chara.currentHealth / chara.getMaxHealth();
            bufferHealthGauges[chara.name].value = chara.currentHealth / chara.getMaxHealth();
        }
    }

    private void initializeGauges(List<Character> characters, List<GameObject> gaugesList) {
        if(characters.Count > gaugesList.Count) {
            Debug.LogError("ERROR: More character than possibly displayed. Maximum " + gaugesList.Count + ", got " + characters.Count);
        }
        for(int i=0; i < characters.Count; i++) {
            gaugesList[i].SetActive(true);
            gaugesList[i].GetComponent<Text>().text = characters[i].name.ToUpper();
            atbGauges.Add(characters[i].name, gaugesList[i].FindComponentInChildWithTag<Slider>("ATB"));
            healthGauges.Add(characters[i].name, gaugesList[i].FindComponentInChildWithTag<Slider>("HealthBar"));
            bufferHealthGauges.Add(characters[i].name, gaugesList[i].FindComponentInChildWithTag<Slider>("BufferHealth"));
        }
    }

    private void initializeCharactersGraphics() {
        foreach(Character character in combatManager.characters) {
            GameObject prefab = GameObject.Instantiate((GameObject)Resources.Load("Characters/Prefabs/" + character.name));
            if (!prefab) Debug.LogError(character.name + " not found in Characters/Prefab/");
            if (!character.isAlly) {
                prefab.transform.localScale = new Vector3(prefab.transform.localScale.x * - 1, prefab.transform.localScale.y, prefab.transform.localScale.z); //if it is an ennemy, we simply reverse it
            }
            CharacterCombat currentCharacterCombat = prefab.GetComponentInChildren<CharacterCombat>();
            currentCharacterCombat.setCharacName(character.name);
            charactersCombat.Add(currentCharacterCombat);
        }
    }

    public List<CharacterCombat> getCharactersCombat() {
        return this.charactersCombat;
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
        return atbGauges[chara];
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

    public void getDamaged(string characterName) {
        Character damagedCharacter = combatManager.getCharacterFromName(characterName);
        float oldHealthSliderValue = healthGauges[characterName].value;
        float newHealthSliderValue = (float)damagedCharacter.currentHealth / (float)damagedCharacter.getMaxHealth();
        healthGauges[characterName].value = newHealthSliderValue;
        StopCoroutine(healthDiminution(characterName, oldHealthSliderValue));
        StartCoroutine(healthDiminution(characterName, oldHealthSliderValue));
    }

    IEnumerator healthDiminution(string character, float oldSliderValue) {
        Debug.Log(character+", old life: "+oldSliderValue+", new life: "+ healthGauges[character].value);
        float timeToPlayAnim = (oldSliderValue - healthGauges[character].value) * 2;
        float timer = 0;
        while (bufferHealthGauges[character].value > healthGauges[character].value) {
            timer += Time.deltaTime;
            bufferHealthGauges[character].value = oldSliderValue - looseHealthSpeedAnimation.Evaluate(timer / timeToPlayAnim);
            if (bufferHealthGauges[character].value <= 0) {
                Character chara = combatManager.getCharacterFromName(character);

                //kill the character
                combatManager.characters.Remove(chara);
                Destroy(bufferHealthGauges[character].gameObject.transform.parent);

                //TODO do something better with end of fight transitions
                if(combatManager.getAlliesList().Count == 0) {
                    Debug.Log("GAME OVER");
                    Destroy(combatManager.gameObject);
                    SceneManager.LoadScene("Scenes/TitleScreen");
                } else if(combatManager.getEnemiesList().Count == 0) {
                    SceneManager.LoadScene("Scenes/Overworld");
                    Debug.Log("YOU WIN");
                }

            }
            yield return null;
        }
    }
}
