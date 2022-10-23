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
    public List<Stat> Health = new List<Stat>();
    public List<Stat> Mana = new List<Stat>();
    public List<Stat> Defense = new List<Stat>();
    public List<Stat> Attack = new List<Stat>();
    public List<Stat> CritRate = new List<Stat>();
    public List<Stat> CritDamage = new List<Stat>();
    public List<Stat> Dodge = new List<Stat>();

}
