using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Goddess.PlayerStat;

namespace Goddess.Item
{
    public class Weapon : Item, IItem
    {
        Stat mod1, mod2;

        public void Equip()
        {
            player.Health.AddModifier(new Stat(100, StatType.Flat, this));
            player.Health.AddModifier(new Stat(100, StatType.PercentAdd, this));
        }

        public void Unequip()
        {
            player.Health.RemoveAllModifierFromSource(this);
        }
    }
}
