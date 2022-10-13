using Goddess.PlayerStat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item Data", menuName = "Item Data", order = 50)]
public class ItemData : ScriptableObject
{
    public string itemName = "Item Name";
    public Sprite icon;
    [TextArea]
    public string description = "";
    public int recoverHealth = 0;
    public int recoverMana = 0;
    public List<Stat> Health;
    public List<Stat> Mana;
    public List<Stat> Defense;
    public List<Stat> Attack;
    public List<Stat> CritRate;
    public List<Stat> CritDamage;
    public List<Stat> Dodge;

}
