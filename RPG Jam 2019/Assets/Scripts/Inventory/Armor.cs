using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armor : MonoBehaviour
{
    public new string name { get; set; }
    public string decs { get; set; }
    public int price { get; set; }
    public int lvl { get; set; }

    public int baseDamage { get; set; }

    public int strengthModificator { get; set; }
    public int dexterityModificator { get; set; }
    public int intellectModificator { get; set; }
    public int accuracyModificator { get; set; }

    public int defense;
}
