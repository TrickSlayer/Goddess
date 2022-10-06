namespace Goddess.PlayerStat
{
    public enum StatModifyType
    {
        Flat = 100,
        PercentAdd = 200,
        FlatBonus = 300,
        PercentMut = 400,
    }

    public class StatModify
    {
        public readonly float Value;
        public readonly StatModifyType Type;
        public readonly int Order;
        public readonly object Source;

        public StatModify(float value, StatModifyType type, int order, object source)
        {
            Value = value;
            Type = type;
            Order = order;
            Source = source;
        }

        public StatModify(float value, StatModifyType type) : this(value, type, (int)type, null) { }

        public StatModify(float value, StatModifyType type, int order) : this(value, type, order, null) { }

        public StatModify(float value, StatModifyType type, object source) : this(value, type, (int)type, source) { }
    }
}

