using System;

namespace Gamification.Web.Utils.CommonViewModels
{
    public class NumericSelectListItem : IComparable<NumericSelectListItem>
    {
        public NumericSelectListItem()
        {
        }

        public NumericSelectListItem(int value, string text) 
            : this(value, text, false)
        {
        }

        public NumericSelectListItem(int value, string text, bool isSelected)
        {
            Value = value;
            Text = text;
            Selected = isSelected;
        }

        public int Value { get; set; }

        public string Text { get; set; }

        public bool Selected { get; set; }

        public bool Equals(NumericSelectListItem other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.Value == Value;
        }

        public int CompareTo(NumericSelectListItem other)
        {
            return Value - other.Value;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(NumericSelectListItem)) return false;
            return Equals((NumericSelectListItem)obj);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        private static readonly Lazy<NumericSelectListItem> _empty = new Lazy<NumericSelectListItem>(() => new NumericSelectListItem(-1, string. Empty));
        public static NumericSelectListItem Empty
        {
            get { return _empty.Value; }
        }
    }
}
