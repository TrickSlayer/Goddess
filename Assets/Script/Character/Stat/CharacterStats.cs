using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Goddess.PlayerStat;
using Random = System.Random;

public class CharacterStats : MonoBehaviour
{
    public CharacterStat Health = new CharacterStat(0);
    public CharacterStat Mana = new CharacterStat(0);
    public CharacterStat Defense = new CharacterStat(0);
    public CharacterStat Attack = new CharacterStat(0);
    public CharacterStat CritRate = new CharacterStat(0);
    public CharacterStat CritDamage = new CharacterStat(0);
    public CharacterStat Dodge = new CharacterStat(0);

    private void Awake()
    {
        currentHealth = Health.Value;
        currentMana = Mana.Value;
    }

    // Start is called before the first frame update
    public virtual void Start()
    {
        SetStartHealth();
        SetStartMana();
        checkDie();
    }

    // Update is called once per frame
    public virtual void Update()
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

    #region slider
    public InformationBar HealthBar;
    public InformationBar ManaBar;

    [HideInInspector] public int currentHealth { get; private set; }
    [HideInInspector] public int currentMana { get; private set; }

    protected void SetStartHealth()
    {
        HealthBar.SetMaxValue(Health.Value);
        HealthBar.SetValue(currentHealth);
    }

    protected void SetStartMana()
    {
        ManaBar.SetMaxValue(Mana.Value);
        ManaBar.SetValue(currentMana);
    }

    public void TakeDamage(int damage)
    {
        Random ran = new System.Random();
        int rate = ran.Next(0, 100);
        if (rate < Dodge.Value)
        {
            Debug.Log("Attack Player Miss");
            return;
        }

        damage -= Defense.Value;

        currentHealth -= Mathf.Clamp(damage, 0, int.MaxValue);

        HealthBar.SetValue(currentHealth);

        checkDie();
    }

    private void checkDie()
    {
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
    }

    public int AttackEnemy()
    {
        int damage = Attack.Value;
        Random ran = new System.Random();
        int rate = ran.Next(0, 100);
        if (rate < CritRate.Value)
        {
            damage *= CritDamage.Value / 100;
        }
        return damage;
    }

    void UseSkill(int mana)
    {
        currentMana -= mana;

        ManaBar.SetValue(currentMana);
    }

    public void RecoverHealth(int health)
    {
        currentHealth += health;
        if (currentHealth > Health.Value)
        {
            currentHealth = Health.Value;
        }
        HealthBar.SetValue(currentHealth);
    }

    public void RecoverMana(int mana)
    {
        currentMana += mana;
        if(currentMana > Mana.Value)
        {
            currentMana = Mana.Value;
        }
        ManaBar.SetValue(currentMana);
    }

    public void SetHealth(int health)
    {
        currentHealth = health;
        if (currentHealth > Health.Value)
        {
            currentHealth = Health.Value;
        }
        HealthBar.SetValue(currentHealth);
    }

    public void SetMana(int mana)
    {
        currentMana = mana;
        if (currentMana > Mana.Value)
        {
            currentMana = Mana.Value;
        }
        ManaBar.SetValue(currentMana);
    }
    #endregion

    public virtual void Die()
    {

    }
}
