using System;

namespace Gamification.Core.Entities
{
    [Serializable]
    public abstract class BaseEntity
    {
        public virtual int Id { get; protected set; }

        public bool Equals(BaseEntity other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.Id == Id;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((BaseEntity) obj);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("{0}#{1}", GetType().Name, this.Id);
        }
    }
}
