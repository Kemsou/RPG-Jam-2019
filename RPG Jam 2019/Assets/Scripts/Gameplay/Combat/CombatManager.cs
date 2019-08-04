using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DamagesInfo {
    public Character target;
    public Dictionary<DamageType, int> damages;
    public Dictionary<AlterationType, int> inflictedAlterations; //when calculating the alteration chance, the int correspond to the power of the effect.
                                                                 //When inflicting the alteration, we simply don't look at the int. If it is in the dictionary, we inflict it

    public DamagesInfo(Dictionary<DamageType, int> damages, Dictionary<AlterationType, int> inflictedAlterations, Character target) {
        this.damages = damages;
        this.inflictedAlterations = inflictedAlterations;
        this.target = target;
    }
}

public enum ActType { Attack, Skill }

public class ActInfo {
    public List<DamagesInfo> damagesInfo = new List<DamagesInfo>();
    public ActType actType; //will be useful to determine which animation to play
    public string characterName; //will be useful to know which character attacks

    public ActInfo(DamagesInfo dmgInfo, ActType actType, string characterName) {
        damagesInfo.Add(dmgInfo);
        this.actType = actType;
        this.characterName = characterName;
    }

    public ActInfo(List<DamagesInfo> dmgInfo, ActType actType, string characterName) {
        damagesInfo = dmgInfo;
        this.actType = actType;
        this.characterName = characterName;
    }
}

//Singleton CombatManager that can pass data through Overworld and Combat
public class CombatManager : MonoBehaviour
{
    public static CombatManager i;

    private CombatUIManager combatUIManager;

    public bool debugAllies = false; //Set this to true to get the dark guy and the light girl at start

    public List<Character> characters;

    private Queue<ActInfo> charactersWaitingToAct = new Queue<ActInfo>(); //every character waiting to launch an attack is put in this list to be handled later
    public ActInfo actingCharacter = null;

    private int waitingCharacterDamaged = 0;

    public Character getCharacterFromName(string name) {
        foreach (Character chara in characters) {
            if (chara.name == name) {
                return chara;
            }
        }
        Debug.LogError("character " + name + " not found in combat manager");
        return null;
    }

    public CharacterCombat getCharacterCombatFromName(string name) {
        foreach (CharacterCombat chara in combatUIManager.getCharactersCombat()) {
            if (chara.getCharacName() == name) {
                return chara;
            }
        }
        Debug.LogError("character combat " + name + " not found in combat manager");
        return null;
    }

    public void initializeCombat(CombatUIManager combatUIManager) {
        this.combatUIManager = combatUIManager;
    }

    public Character getRandomEnemy() {
        List<Character> tmp = new List<Character>();
        foreach(Character chara in characters) {
            if (!chara.isAlly) tmp.Add(chara);
        }
        return tmp[Random.Range(0, tmp.Count)];
    }

    public Character getRandomAlly() {
        List<Character> tmp = new List<Character>();
        foreach (Character chara in characters) {
            if (chara.isAlly) tmp.Add(chara);
        }
        return tmp[Random.Range(0, tmp.Count)];
    }

    //The action parameter correspond to either a weapon (if it is a basic attack) or a Skill. We then determine the damages from this
    public void makeCharacterAct(string actingCharacterName, Object action, List<Character> targets) {
        Character actingCharacter = getCharacterFromName(actingCharacterName);
        List<DamagesInfo> listDamages = new List<DamagesInfo>();//one DamagesInfo by target (eahc target will receive different damages because of its armor)
        if (action is Weapon){
            //it is a basic attack
            Weapon charaWeapon = actingCharacter.weapon;
            foreach(Character target in targets) {
                listDamages.Add(calculateDamageTaken(Utility.getDamagesInfo(charaWeapon.damages, charaWeapon.characMultiplier, charaWeapon.alterations, actingCharacter, target), target));
            }
            charactersWaitingToAct.Enqueue(new ActInfo(listDamages, ActType.Attack, actingCharacterName));
        } else if(action is Skill) {
            //it is a skill
            Skill skill = (Skill)action;
            foreach(SkillProperty property in skill.properties) {
                foreach (Character target in targets) {
                    listDamages.Add(calculateDamageTaken(Utility.getDamagesInfo(property.damages, property.characMultiplier, property.alterations, actingCharacter, target), target));
                }
            }
            charactersWaitingToAct.Enqueue(new ActInfo(listDamages, ActType.Skill, actingCharacterName));
        } else {
            Debug.LogError(actingCharacterName + " was asked to act with unknown action: " + action.ToString());
        }
    }

    private void Awake() {
        if (!i) {
            i = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    //return how much damages the character takes depending on the entry damages (we substract the resistance)
    private DamagesInfo calculateDamageTaken(DamagesInfo entryDamage, Character character) {
        DamagesInfo result = new DamagesInfo(new Dictionary<DamageType, int>(), new Dictionary<AlterationType, int>(), character);
        Armor defenserArmor = character.armor;
        foreach(KeyValuePair<DamageType, int> damage in entryDamage.damages) {
            result.damages.Add(damage.Key, damage.Value - defenserArmor.getArmorResistance(damage.Key));//we substract the resistance
        }
        //TODO something with the alterations
        return result;
    }

    public void damageAnimationFinished(string name) {
        waitingCharacterDamaged--;
        if(waitingCharacterDamaged == 0) {
            actingCharacter = null;
            combatUIManager.isATBTimeFlowing = true; //ATB can now flow again
        }
    }

    public List<Character> getAlliesList() {
        List<Character> ret = new List<Character>();
        foreach(Character chara in characters) {
            if (chara.isAlly) ret.Add(chara);
        }
        return ret;
    }

    public List<Character> getEnemiesList() {
        List<Character> ret = new List<Character>();
        foreach (Character chara in characters) {
            if (!chara.isAlly) ret.Add(chara);
        }
        return ret;
    }

    //we make the enemy choose an action and attack
    public void enemyChooseAction(string enemyName) {
        //we attack a single random ally with our weapon
        List<Character> targets = new List<Character>();
        targets.Add(getRandomAlly()); //TODO: the targets must depend on the attack range
        makeCharacterAct(enemyName, getCharacterFromName(enemyName).weapon, targets);

        //TODO: do something else than a simple attack on a random ally. The enemy must be able to choose if he heals (if he can), or which ally to attack etc...

    }

    public void getDamaged(string character, int damages) {
        Character chara = getCharacterFromName(character);
        chara.currentHealth -= damages;
        combatUIManager.getDamaged(character);
    }

    private void Update() {
        if (actingCharacter == null && charactersWaitingToAct.Count > 0) {

            actingCharacter = charactersWaitingToAct.Dequeue();

            //we addd every targets of the act into the damagedTargets
            waitingCharacterDamaged = actingCharacter.damagesInfo.Count;

            //send the act to the component in charge of the animation
            foreach (CharacterCombat characterCombat in combatUIManager.getCharactersCombat()) {
                if (characterCombat.getCharacName() == actingCharacter.characterName) {
                    characterCombat.act(actingCharacter);
                    combatUIManager.isATBTimeFlowing = false; //we stop ATB during attack animation
                }
            }
        }
    }
    

    private void Start() {
        if (debugAllies) {
            Character cloud = Utility.loadNewCharacter("Cloud");
            cloud.currentHealth = cloud.getMaxHealth();
            characters.Add(cloud);
        }
    }
}

