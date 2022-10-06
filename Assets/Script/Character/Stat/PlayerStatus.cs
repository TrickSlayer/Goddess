using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Goddess.PlayerStat
{
    [Serializable]
    public class PlayerStat
    {
        public float BaseValue;
        protected bool isDirty = true;                        // re caculate final value when statModifies change
        protected int _value;
        protected readonly List<StatModify> statModifies;
        public readonly ReadOnlyCollection<StatModify> StatModifies;
        protected float lastBaseValue = float.MinValue;

        public virtual int Value
        {
            get
            {
                if (isDirty || BaseValue != lastBaseValue)
                {
                    lastBaseValue = BaseValue;
                    _value = CaculatateFinalValue();
                    isDirty = false;
                }
                return _value;
            }
        }

        public PlayerStat()
        {
            statModifies = new List<StatModify>();
            StatModifies = statModifies.AsReadOnly();
        }

        public PlayerStat(float baseValue) : this()
        {
            BaseValue = baseValue;
        }

        public virtual void AddModifier(StatModify mod)
        {
            isDirty = true;
            statModifies.Add(mod);
            statModifies.Sort(CompareModifilerOrder);
        }

        public virtual bool RemoveModifier(StatModify mod)
        {
            if (statModifies.Remove(mod))
            {
                isDirty = true;
                return true;
            }
            return false;
        }

        public virtual bool RemoveAllModifierFromSource(object source)
        {
            bool remove = false;
            for (int i = statModifies.Count - 1; i >= 0; i--)
            {
                if (statModifies[i].Source == source)
                {
                    isDirty = true;
                    remove = true;
                    statModifies.RemoveAt(i);
                }
            }
            return remove;
        }

        protected virtual int CompareModifilerOrder(StatModify a, StatModify b)
        {
            if (a.Order < b.Order)
            {
                return -1;
            }
            else if (a.Order > b.Order)
            {
                return 1;
            }
            return 0;
        }

        protected virtual int CaculatateFinalValue()
        {
            float final = BaseValue;
            float sumPercentAdd = 0;
            for (int i = 0; i < statModifies.Count; i++)
            {
                StatModify mod = statModifies[i];

                switch (mod.Type)
                {
                    case StatModifyType.Flat:
                    case StatModifyType.FlatBonus:
                        final += mod.Value;
                        break;
                    case StatModifyType.PercentAdd:
                        sumPercentAdd += mod.Value;
                        if (i == statModifies.Count - 1 || statModifies[i + 1].Type != StatModifyType.PercentAdd)
                        {
                            final *= 1 + sumPercentAdd / 100;
                            sumPercentAdd = 0;
                        }
                        break;
                    case StatModifyType.PercentMut:
                        final *= 1 + mod.Value / 100;
                        break;
                }


            }

            return (int) Math.Round(final);
        }
    }
}
