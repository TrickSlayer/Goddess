using Goddess.PlayerStat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Enemy Data", menuName = "Enemy Data", order = 50)]
public class EnemyData : ScriptableObject
{
    public string enemyName = "Enemy Name";
    public CharacterStat Health;
    [HideInInspector] public int currentHealth;
    public CharacterStat Defense;
    public CharacterStat Attack;
    public CharacterStat Dodge;
    public int Experience;
}
