using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Goddess.PlayerStat;

public class CharacterStats : MonoBehaviour
{
    public CharacterStat Health;
    public CharacterStat Mana;
    public CharacterStat Defense;
    public CharacterStat Attack;
    public CharacterStat CritRate;
    public CharacterStat CritDamage;
    public CharacterStat Dodge;

    private void Awake()
    {
        HealthBar   = GameObject.FindGameObjectWithTag("CharacterHealthBar").GetComponent<HealthBar>();
        ManaBar     = GameObject.FindGameObjectWithTag("CharacterManaBar").GetComponent<ManaBar>();
        currentHealth = Health.Value;
        currentMana = Mana.Value;
    }

    // Start is called before the first frame update
    public virtual void Start()
    {
        SetStartHealth();
        SetStartMana();
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
    private HealthBar HealthBar;
    private ManaBar ManaBar;

    [HideInInspector] public int currentHealth { get; private set; }
    [HideInInspector] public int currentMana { get; private set; }

    protected void SetStartHealth()
    {
        HealthBar.SetMaxHealth(Health.Value);
        HealthBar.SetHealth(currentHealth);
    }

    protected void SetStartMana()
    {
        ManaBar.SetMaxMana(Mana.Value);
        ManaBar.SetMana(currentMana);
    }

    void TakeDamage(int damage)
    {
        damage -= Defense.Value;

        currentHealth -= Mathf.Clamp(damage, 0, int.MaxValue);

        HealthBar.SetHealth(currentHealth);

        if (currentHealth < 0)
        {
            Die();
        }
    }

    void UseSkill(int mana)
    {
        currentMana -= mana;

        ManaBar.SetMana(currentMana);
    }

    public void RecoverHealth(int health)
    {
        currentHealth += health;
        HealthBar.SetHealth(currentHealth);
    }

    public void RecoverMana(int mana)
    {
        currentMana += mana;
        ManaBar.SetMana(currentMana);
    }


    #endregion

    public virtual void Die()
    {

    }
}
