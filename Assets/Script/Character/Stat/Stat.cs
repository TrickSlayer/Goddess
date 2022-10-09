using System.Collections.Generic;
using System;
using System.Collections.ObjectModel;

namespace Goddess.PlayerStat
{
    public enum StatType
    {
        Flat = 100,
        PercentAdd = 200,
        FlatBonus = 300,
        PercentMut = 400,
    }

    public class Stat
    {
        public float Value;
        public StatType Type;
        public int Order;
        public object Source;

        public Stat(float value, StatType type, int order, object source)
        {
            Value = value;
            Type = type;
            Order = order;
            Source = source;
        }

        public Stat(float value, StatType type) : this(value, type, (int)type, null) { }

        public Stat(float value, StatType type, int order) : this(value, type, order, null) { }

        public Stat(float value, StatType type, object source) : this(value, type, (int)type, source) { }
    }

    [Serializable]
    public class CharacterStat
    {
        public float BaseValue;
        protected bool isDirty = true;                                  // re caculate final value when stats change
        protected int _value;
        protected List<Stat> stats { get; private set; }
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

        public CharacterStat()
        {
            stats = new List<Stat>();
        }

        public CharacterStat(float baseValue) : this()
        {
            BaseValue = baseValue;
        }

        public virtual void AddModifier(Stat mod)
        {
            isDirty = true;
            stats.Add(mod);
            stats.Sort(CompareModifilerOrder);
        }

        public virtual void AddModifier(CharacterStat stats)
        {
            foreach (Stat mod in stats.stats)
            {
                AddModifier(mod);
            }
        }

        public virtual bool RemoveModifier(Stat mod)
        {
            if (stats.Remove(mod))
            {
                isDirty = true;
                return true;
            }
            return false;
        }

        public virtual void RemoveModifier(CharacterStat stats)
        {
            foreach (Stat mod in stats.stats)
            {
                RemoveModifier(mod);
            }
        }

        public virtual bool RemoveAllModifierFromSource(object source)
        {
            bool remove = false;
            for (int i = stats.Count - 1; i >= 0; i--)
            {
                if (stats[i].Source == source)
                {
                    isDirty = true;
                    remove = true;
                    stats.RemoveAt(i);
                }
            }
            return remove;
        }

        protected virtual int CompareModifilerOrder(Stat a, Stat b)
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
            for (int i = 0; i < stats.Count; i++)
            {
                Stat mod = stats[i];

                switch (mod.Type)
                {
                    case StatType.Flat:
                    case StatType.FlatBonus:
                        final += mod.Value;
                        break;
                    case StatType.PercentAdd:
                        sumPercentAdd += mod.Value;
                        if (i == stats.Count - 1 || stats[i + 1].Type != StatType.PercentAdd)
                        {
                            final *= 1 + sumPercentAdd / 100;
                            sumPercentAdd = 0;
                        }
                        break;
                    case StatType.PercentMut:
                        final *= 1 + mod.Value / 100;
                        break;
                }


            }

            return (int)Math.Round(final);
        }
    }
}

