using System.Collections.Generic;

public class CharacterData : Data
{

    public string name { get; set; }
    public int lvl { get; set; }
    public bool isAlly { get; set; }
    public int currentHealth { get; set; }
    public int currentMana { get; set; }
    public List<string> inventoryNames { get; set; }
    public string armorName { get; set; }
    public string weaponName { get; set; }
    public int gold { get; set; }

    public CharacterData(string sceneName, string gameObjectName, string componentName, string valueName, string value, string type,
    string name, int lvl, bool isAlly, int currentHealth, int currentMana, List<string> inventoryNames, string armorName, string weaponName, int gold) :
    base(sceneName, gameObjectName, componentName, valueName, value, type)
    {
    this.name = name;
    this.lvl = lvl;
    this.isAlly = isAlly;
    this.currentHealth = currentHealth;
    this.currentMana = currentMana;
    this.inventoryNames = inventoryNames;
    this.armorName = armorName;
    this.weaponName = weaponName;
    this.gold = gold;
    }


}
