using Goddess.PlayerStat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerStats : CharacterStats
{
    [HideInInspector] public static PlayerStats instance;

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

    public void OnEquipmentChanged(Item newItem, Item oldItem)
    {
        if (newItem != null)
        {
            Health.AddModifier(newItem.data.Health);
            Mana.AddModifier(newItem.data.Mana);
            Defense.AddModifier(newItem.data.Defense);
            Attack.AddModifier(newItem.data.Attack);
            CritRate.AddModifier(newItem.data.CritRate);
            CritDamage.AddModifier(newItem.data.CritDamage);
            Dodge.AddModifier(newItem.data.Dodge);
            RecoverHealth(newItem.data.recoverHealth);
            RecoverMana(newItem.data.recoverMana);
        }

        if (oldItem != null)
        {
            Health.RemoveModifier(oldItem.data.Health);
            Mana.RemoveModifier(oldItem.data.Mana);
            Defense.RemoveModifier(oldItem.data.Defense);
            Attack.RemoveModifier(oldItem.data.Attack);
            CritRate.RemoveModifier(oldItem.data.CritRate);
            CritDamage.RemoveModifier(oldItem.data.CritDamage);
            Dodge.RemoveModifier(oldItem.data.Dodge);
        }

        SetStartHealth();
        SetStartMana();
    }

    [HideInInspector] public bool wasDie = false;

    public override void Die()
    {
        base.Die();
        PlayerManager.instance.movementP.isHurt(true);
        wasDie = true;
        Debug.Log(PlayerManager.instance.player.tag);
        PlayerManager.instance.player.gameObject.tag = "Untagged";
        Revival_UI.instance.gameObject.SetActive(true);
    }
}
