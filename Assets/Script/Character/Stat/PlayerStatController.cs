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
    [HideInInspector] public int currentHealth;
    public PlayerStat Mana;
    [HideInInspector] public int currentMana;

    public HealthBar HealthBar;
    public ManaBar ManaBar;

    // Start is called before the first frame update
    void Start()
    {
        Weapon w = new Weapon();
        w.Equip();
        currentHealth = Health.Value;
        currentMana = Mana.Value;
        HealthBar.SetMaxHealth(Health.Value);
        ManaBar.SetMaxMana(Mana.Value);
        HealthBar.SetHealth(currentHealth);
        ManaBar.SetMana(currentMana);
        Debug.Log(Health.Value + " " + currentHealth);
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
