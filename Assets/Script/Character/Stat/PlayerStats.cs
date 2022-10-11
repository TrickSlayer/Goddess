using Goddess.PlayerStat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerStats : CharacterStats
{
    Camera cam;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        cam = Camera.main;
        /* EquipmentManager.instance.onEquipmentChanged += OnEquipmentChanged;*/
    }

    public override void Update()
    {
        base.Update();

    }

    void OnEquipmentChanged(Equipment newItem, Equipment oldItem)
    {
        if (newItem != null)
        {
            Health.AddModifier(newItem.Health);
            Mana.AddModifier(newItem.Mana);
            Defense.AddModifier(newItem.Defense);
            Attack.AddModifier(newItem.Attack);
            CritRate.AddModifier(newItem.CritRate);
            CritDamage.AddModifier(newItem.CritDamage);
            Dodge.AddModifier(newItem.Dodge);
        }

        if (oldItem != null)
        {
            Health.RemoveModifier(oldItem.Health);
            Mana.RemoveModifier(oldItem.Mana);
            Defense.RemoveModifier(oldItem.Defense);
            Attack.RemoveModifier(oldItem.Attack);
            CritRate.RemoveModifier(oldItem.CritRate);
            CritDamage.RemoveModifier(oldItem.CritDamage);
            Dodge.RemoveModifier(oldItem.Dodge);
        }
    }
}
