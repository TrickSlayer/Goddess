using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Goddess.PlayerStat;
using Goddess.Item;

public class PlayerStatController : MonoBehaviour
{
    #region slider
    public PlayerStat Health;
    public PlayerStat Mana;
    public PlayerStat Defense;
    public PlayerStat Attack;
    public PlayerStat CritRate;
    public PlayerStat CritDamage;
    public PlayerStat Dodge;

    [HideInInspector] public int currentHealth;
    [HideInInspector] public int currentMana;

    public HealthBar HealthBar;
    public ManaBar ManaBar;

    // Start is called before the first frame update
    void Start()
    {
        Weapon w = new Weapon();
        w.Equip();

        SetStartHealth();
        SetStartMana();
    }

    void SetStartHealth()
    {
        currentHealth = Health.Value;
        HealthBar.SetMaxHealth(Health.Value);
        HealthBar.SetHealth(currentHealth);
    }

    void SetStartMana()
    {
        currentMana = Mana.Value;
        ManaBar.SetMaxMana(Mana.Value);
        ManaBar.SetMana(currentMana);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            TakeDamage(20);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            UseSkill(20);
        }
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;

        HealthBar.SetHealth(currentHealth);
    }

    void UseSkill(int mana)
    {
        currentMana -= mana;

        ManaBar.SetMana(currentMana);
    }

    #endregion

}
