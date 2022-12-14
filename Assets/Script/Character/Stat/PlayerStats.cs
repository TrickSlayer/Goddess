using Goddess.PlayerStat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerStats : CharacterStats
{
    [HideInInspector] public static PlayerStats instance;

    public int Level = 1;
    public int Score = 1;
    public CharacterStat Experience = new CharacterStat(100);
    public int currentExperience = 0;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        /* EquipmentManager.instance.onEquipmentChanged += OnEquipmentChanged;*/
    }

    public override void Update()
    {
        base.Update();

    }

    public void gainExperience(int exp)
    {
        currentExperience += exp;
        if (currentExperience >= Experience.Value)
        {
            currentExperience -= Experience.Value;
            Level += 1;
            Experience.AddModifier(new Goddess.PlayerStat.Stat(50, Goddess.PlayerStat.StatType.PercentMut));
            Score += Level;
        }
    }

    public void OnEquipmentChanged(Item newItem, Item oldItem)
    {
        if (newItem != null)
        {

            Health.AddModifier(newItem.data.Health);

            Mana.AddModifier(newItem.data.Mana);

            Defense.AddModifier(newItem.data.Defense);

            Attack.AddModifier(newItem.data.Attack);

            Dodge.AddModifier(newItem.data.Dodge);

            CritRate.AddModifier(newItem.data.CritRate);

            CritDamage.AddModifier(newItem.data.CritDamage);

            RecoverHealth(newItem.data.recoverHealth);
            RecoverMana(newItem.data.recoverMana);
        }

        if (oldItem != null)
        {

            Health.RemoveModifier(newItem.data.Health);

            Mana.RemoveModifier(newItem.data.Mana);

            Defense.RemoveModifier(newItem.data.Defense);

            Attack.RemoveModifier(newItem.data.Attack);

            CritRate.RemoveModifier(newItem.data.CritRate);

            CritDamage.RemoveModifier(newItem.data.CritDamage);

            Dodge.RemoveModifier(newItem.data.Dodge);
        }

        SetStartHealth();
        SetStartMana();
    }

    [HideInInspector] public bool wasDie = false;

    public override void Die()
    {
        base.Die();
        currentExperience = 0;
        StatusAttack.instance.ShowMess("- " + currentExperience, Color.green);
        PlayerManager.instance.movementP.isHurt(true);
        wasDie = true;
        PlayerManager.instance.player.gameObject.tag = "Untagged";
        Revival_UI.instance.gameObject.SetActive(true);
    }
}
