using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Weapon : IItem
{

    #region Item

    public string name { get; set; }
    public string decs { get; set; }
    public int price { get; set; }
    public int lvl { get; set; }

    #endregion

    #region Weapon

    public int lifeBonus { get; set; }
    public int strengthBonus { get; set; }
    public int dexterityBonus { get; set; }
    public int intellectBonus { get; set; }
    public int accuracyBonus { get; set; }

    public int strengthModificator { get; set; }
    public int dexterityModificator { get; set; }
    public int intellectModificator { get; set; }
    public int accuracyModificator { get; set; }

    #endregion


}
