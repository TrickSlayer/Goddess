using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int maxHealth = 1000;
    [HideInInspector] public int currentHealth;
    public int maxMana = 1000;
    [HideInInspector] public int currentMana;

    public HealthBar healthBar;
    public ManaBar manaBar;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        currentMana = maxMana;
        healthBar.SetMaxHealth(maxHealth);
        manaBar.SetMaxMana(maxMana);
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

        healthBar.SetHealth(currentHealth);
    }

    void UseSkill(int mana)
    {
        currentMana -= mana;

        manaBar.SetMana(currentMana);
    }
}
