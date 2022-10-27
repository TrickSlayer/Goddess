using Goddess.PlayerStat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player Begin Data", menuName = "Player Begin Data", order = 50)]
public class PlayerBeginData : ScriptableObject
{
    public string playerName = "Player Name";
    public int Level = 1;
    public int Score = 1;
    public CharacterStat Experience = new CharacterStat(100);
    public CharacterStat Health = new CharacterStat(0);
    public CharacterStat Mana = new CharacterStat(0);
    public CharacterStat Defense = new CharacterStat(0);
    public CharacterStat Attack = new CharacterStat(0);
    public CharacterStat CritRate = new CharacterStat(0);
    public CharacterStat CritDamage = new CharacterStat(0);
    public CharacterStat Dodge = new CharacterStat(0);
}
